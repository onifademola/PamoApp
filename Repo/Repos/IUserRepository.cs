using Repo.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Repo.Repos
{
    public interface IUserRepository : IRepositoryBase<Role>
    {
        IEnumerable GetList(string name);
        User FindUser(string id);
        void UpdateUser(User user);
        IList GetUsers();
        IQueryable AppRoles();
        IEnumerable<AspNetRole> AppERoles();
    }
}