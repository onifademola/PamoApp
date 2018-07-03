using System.Collections;
using System.Linq;
using System.Data.Entity;
using Repo.Models;
using System.Collections.Generic;
using Repo.Services;

namespace Repo.Repos
{
    public class UserRepository : RepositoryBase<PamoDbEntities, Role>, IUserRepository
    {
        public IEnumerable GetList(string name)
        {
            var rList = ""; // _entities.vwUserViews.Where(x => x.Name == name).ToList();
            return rList;
        }

        public User FindUser(string id)
        {
            User user = _entities.Users.FirstOrDefault(i => i.Id == id);
            return user;
            //_entities.Entry(user).State = EntityState.Modified;
            //_entities.SaveChanges();
        }
        public void UpdateUser(User user)
        {
            _entities.Entry(user).State = EntityState.Modified;
        }

        public IList GetUsers()
        {
            var list = _entities.Users.ToList();
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
