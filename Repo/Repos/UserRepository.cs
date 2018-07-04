using System.Collections;
using System.Linq;
using System.Data.Entity;
using Repo.Models;
using System.Collections.Generic;
using Repo.Services;
using Repo.DTOs;
using AutoMapper.QueryableExtensions;

namespace Repo.Repos
{
    public class UserRepository : RepositoryBase<PamoDbEntities, Role>, IUserRepository
    {
        public IEnumerable GetList(string name)
        {
            var rList = ""; // _entities.vwUserViews.Where(x => x.Name == name).ToList();
            return rList;
        }

        public AspNetUser FindUser(string id)
        {
            AspNetUser user = _entities.AspNetUsers.FirstOrDefault(i => i.Id == id);
            return user;
            //_entities.Entry(user).State = EntityState.Modified;
            //_entities.SaveChanges();
        }
        public void UpdateUser(AspNetUser user)
        {
            _entities.Entry(user).State = EntityState.Modified;
        }

        public IList GetUsers()
        {
            var list = _entities.AspNetUsers.ToList();
            return list;
        }

        public IQueryable<dto_AspNetUser> GetDtoUsers()
        {
            var allUsers = _entities.AspNetUsers.ProjectTo<dto_AspNetUser>();
            return allUsers;
        }

        public IQueryable GetAppUsers()
        {
            var list = _entities.AspNetUsers;
            return list;
        }

        public IQueryable AppRoles()
        {
            var roles = _entities.AspNetRoles.Where(x => x.Name != "SuperAdmin");
            return roles;
        }

        public IEnumerable<AspNetRole> AppERoles()
        {
            var roles = _entities.AspNetRoles.Where(x => x.Name != "SuperAdmin");
            return roles;
        }

        #region RANDOM PWD GENERATOR
        public string GeneratePassword(int lower, int upper, int num, int symbol)
        {
            string pass = PasswordService.GenerateRandomPassword(lower, upper, num, symbol);
            return pass;
        }

        //public string GeneratePassword(int numOfPass, int lower, int upper, int num, int symbol)
        //{
        //    string pass = PasswordService.GenerateRandomPassword(lower, upper, num, symbol);
        //    return pass;
        //}
        #endregion 
    }
}
