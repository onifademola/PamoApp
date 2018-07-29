using Repo.DTOs;
using Repo.Models;
using System.Linq;

namespace Repo.Repos
{
    public class Repo_Consulting : RepositoryBase<PamoDbEntities, attendance>, IRepo_Consulting
    {
        public bool UpdateDocRecAttendance(dto_Attendance attd)
        {
            attendance attends = _entities.attendances.FirstOrDefault(n => n.ID == attd.ID);
            if (attends == null)
                return false;
            attends.Note = attd.Note;
            attends.DoctorID = attd.DoctorID;
            attends.Consultant = attd.Consultant;
            var result = _entities.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }

        //public bool UpdateAttendance(dto_Attendance attd)
        //{
        //    var entity = AutoMapper.Mapper.Map<attendance>(attd);
        //    _entities.Set<attendance>().Attach(entity);
        //    _entities.Entry<attendance>(entity).State = System.Data.Entity.EntityState.Modified;
        //    var result = _entities.SaveChanges();
        //    if (result > 0)
        //        return true;
        //    else
        //        return false;
        //}
    }
}
