using Repo.DTOs;
using Repo.Models;
using System.Linq;

namespace Repo.Repos
{
    public interface IRepo_Consulting : IRepositoryBase<attendance>
    {
        bool UpdateDocRecAttendance(dto_Attendance attd);

        #region Patient's Prescription Repo
        IQueryable<dto_Prescription> GetPrescriptionForAttendance(int attdID);
        RepositoryActionResult<dto_Prescription> AddPrescription(dto_Prescription bio, int attdID);
        RepositoryActionResult<dto_Prescription> EditPrescription(dto_Prescription bio);
        RepositoryActionResult<dto_Prescription> UpdatePrescriptionByPahrm(dto_Prescription bio);
        RepositoryActionResult<dto_Prescription> DeletePrescription(int bioId);
        #endregion
    }
}
