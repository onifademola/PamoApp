using Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Repos
{
    public interface IRepo_Labs : IRepositoryBase<labw>
    {
        IQueryable GetAllLabws();

        bool AddLabw(labw pat);

        bool EditLabw(labw pat);

        bool DeleteLabw(long patId);
    }
}
