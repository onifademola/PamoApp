using Repo.Models;
using System.Collections;

namespace Repo.Repos
{
    public interface IUserRepository : IRepositoryBase<Role>
    {
        IEnumerable GetList(string name);
        User FindUser(string id);
        void UpdateUser(User user);
        IList GetUsers();
    }
}