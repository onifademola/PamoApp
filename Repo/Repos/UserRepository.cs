﻿using System.Collections;
using System.Linq;
using System.Data.Entity;
using Repo.Models;

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
    }
}
