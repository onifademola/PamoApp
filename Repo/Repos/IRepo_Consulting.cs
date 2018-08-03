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

        #region LAB Repo
        IQueryable<dto_LabRequest> GetAllLabRequests();
        IQueryable<dto_LabRequest> GetLabRequestForAttendance(int attdID);
        RepositoryActionResult<dto_LabRequest> AddLab(dto_LabRequest pat, int attdId);
        RepositoryActionResult<dto_LabRequest> EditLab(dto_LabRequest pat);
        RepositoryActionResult<dto_LabRequest> DeleteLab(int labId);
        #endregion

        #region ADMISSION Repo
        IQueryable<dto_Admission> GetAllAdmissions();
        IQueryable<dto_Admission> GetAdmissionForAttendance(int attdID);
        RepositoryActionResult<dto_Admission> AddAdmission(dto_Admission pat, int attdId);
        RepositoryActionResult<dto_Admission> EditAdmission(dto_Admission pat);
        RepositoryActionResult<dto_Admission> DeleteAdmission(int labId);
        #endregion

        #region WARD ROUND Repo
        IQueryable<dto_WardRound> GetAllWardRounds();
        IQueryable<dto_WardRound> GetWardRoundForAdmission(int attdID);
        RepositoryActionResult<dto_WardRound> AddWardRound(dto_WardRound pat, int attdId);
        RepositoryActionResult<dto_WardRound> EditWardRound(dto_WardRound pat);
        RepositoryActionResult<dto_WardRound> DeleteWardRound(int labId);
        #endregion
    }
}
