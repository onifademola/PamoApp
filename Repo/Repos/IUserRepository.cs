using Repo.DTOs;
using Repo.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Repo.Repos
{
    public interface IUserRepository : IRepositoryBase<Role>
    {
        IEnumerable GetList(string name);
        AspNetUser FindUser(string id);
        void UpdateUser(AspNetUser user);
        IList GetUsers();
        IQueryable<dto_AspNetUser> GetDtoUsers();
        IQueryable GetAppUsers();
        IQueryable AppRoles();
        IEnumerable<AspNetRole> AppERoles();
        string GeneratePassword(int lower, int upper, int num, int symbol);
    }
}