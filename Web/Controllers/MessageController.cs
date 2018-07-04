using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Success(string message)
        {
            TempData["success"] = message;
            return View();
        }

        public ActionResult PwdSuccess(string message)
        {
            TempData["success"] = message;
            return View();
        }
        public ActionResult Error(string messaage)
        {

            ViewBag.Message = messaage;
            return View();
        }
    }
}