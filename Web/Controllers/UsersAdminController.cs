using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Repo.Models;
using Repo.Repos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    //[Authorize(Roles = "SuperAdmin, Admin")]
    public class UsersAdminController : Controller
    {
        
        private readonly IAuthenticationManager _authenticationManager;
        public readonly IUserRepository _irepo;
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        
        public UsersAdminController()
        { 

        }

        public UsersAdminController( IAuthenticationManager authenticationManager, IUserRepository irepo)
        {
            _authenticationManager = authenticationManager;
            _irepo = irepo;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        
        //
        // GET: /Users/
        public async Task<ActionResult> Index()
        {
            var model = await UserManager.Users.ToListAsync();
            return View(model);
        }

        //
        // GET: /Users/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var userRoles = await UserManager.GetRolesAsync(user.Id);

            return View(new EditUserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                RolesList = _irepo.AppERoles().ToList().Select(x => new SelectListItem()
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        //
        // GET: /Users/Create
        public async Task<ActionResult> Create()
        {
            //Get the list of Roles
            ViewBag.RoleId = new SelectList(await _irepo.AppRoles().ToListAsync(), "Name", "Name");
            return View();
        }

        //
        // POST: /Users/Create
        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                Guid userId = Guid.NewGuid();
                var user = new ApplicationUser() { UserName = userViewModel.Email, Email = userViewModel.Email, Id = Convert.ToString(userId) };
                //  IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                //var user = new MyUser { UserName = userViewModel.Email, Email = userViewModel.Email };
                var adminresult = await UserManager.CreateAsync(user, userViewModel.Password);

                //Add User to the selected Roles 
                if (adminresult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        var result = await UserManager.AddToRolesAsync(user.Id, selectedRoles);
                        if (!result.Succeeded)
                        {
                            //var appRoles = await _irepo.AppRoles().ToListAsync();
                            ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleId = new SelectList(await _irepo.AppRoles().ToListAsync(), "Name", "Name");
                            return View();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", adminresult.Errors.First());
                    ViewBag.RoleId = new SelectList(_irepo.AppERoles().ToList(), "Name", "Name");
                    return View();

                }
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(_irepo.AppERoles().ToList(), "Name", "Name");
            return View();
        }

        //
        // GET: /Users/Edit/1
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var userRoles = await UserManager.GetRolesAsync(user.Id);

            return View(new EditUserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                RolesList = _irepo.AppERoles().ToList().Select(x => new SelectListItem()
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Email,Id,UserName")] EditUserViewModel editUser, params string[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.UserName = editUser.UserName;
                user.Email = editUser.Email;

                var userRoles = await UserManager.GetRolesAsync(user.Id);

                selectedRole = selectedRole ?? new string[] { };

                var result = await UserManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something failed.");
            var userRolez = await UserManager.GetRolesAsync(editUser.Id);
            return View(new EditUserViewModel()
            {
                Id = editUser.Id,
                UserName = editUser.UserName,
                Email = editUser.Email,
                RolesList = _irepo.AppERoles().ToList().Select(x => new SelectListItem()
                {
                    Selected = userRolez.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        public async Task<ActionResult> ResetPassword(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                //return HttpNotFound();
                string errorMessage = "User login info not found. This user's login may have not been yet created.";
                TempData["message"] = errorMessage;
                return RedirectToAction("ErrorHandler", "Message", new { message = errorMessage });
            }

            var userRoles = await UserManager.GetRolesAsync(user.Id);
            return View(new EditUserPasswordViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                RolesList = _irepo.AppERoles().ToList().Select(x => new SelectListItem()
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword([Bind(Include = "Email,Id,UserName,NewPassword")] EditUserPasswordViewModel editUser, params string[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                PasswordHasher hash = new PasswordHasher();
                var euser = _irepo.FindUser(editUser.Id);
                euser.PasswordHash = hash.HashPassword(editUser.NewPassword);
                _irepo.UpdateUser(euser);
                _irepo.Save();

                //return RedirectToAction("Index");
                string _Message = "User password was successfully updated.";
                TempData["success"] = _Message;
                return RedirectToAction("PwdSuccess", "Message", new { message = _Message });
            }
            ModelState.AddModelError("", "Something failed.");
            var userRolez = await UserManager.GetRolesAsync(editUser.Id);
            return View(new EditUserPasswordViewModel()
            {
                Id = editUser.Id,
                UserName = editUser.UserName,
                Email = editUser.Email,
                RolesList = _irepo.AppERoles().ToList().Select(x => new SelectListItem()
                {
                    Selected = userRolez.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        ////
        //// GET: /Users/Delete/5
        //public async Task<ActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var user = await UserManager.FindByIdAsync(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user);
        //}

        ////
        //// POST: /Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(string id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }

        //        var user = await UserManager.FindByIdAsync(id);
        //        if (user == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        var result = await UserManager.DeleteAsync(user);
        //        if (!result.Succeeded)
        //        {
        //            ModelState.AddModelError("", result.Errors.First());
        //            return View();
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        //public async Task<ActionResult> RoleIndex()
        //{
        //    ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
        //    return View();
        //}

    }
}