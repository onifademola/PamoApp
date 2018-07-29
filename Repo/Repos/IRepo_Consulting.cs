using Repo.DTOs;
using Repo.Models;

namespace Repo.Repos
{
    public interface IRepo_Consulting : IRepositoryBase<attendance>
    {
        bool UpdateDocRecAttendance(dto_Attendance attd);
    }
}
