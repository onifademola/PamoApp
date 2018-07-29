using Repo.DTOs;
using Repo.Models;

namespace Repo.Repos
{
    public class Repo_Consulting : RepositoryBase<PamoDbEntities, attendance>, IRepo_Consulting
    {
        public bool UpdateAttendance(dto_Attendance attd)
        {
            var entity = AutoMapper.Mapper.Map<attendance>(attd);
            _entities.Set<attendance>().Attach(entity);
            _entities.Entry<attendance>(entity).State = System.Data.Entity.EntityState.Modified;
            var result = _entities.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }
    }
}
