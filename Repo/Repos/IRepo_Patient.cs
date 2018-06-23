using Repo.DTOs;
using Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Repos
{
    public interface IRepo_Patient : IRepositoryBase<eregister>
    {
        bool CardNumberExist(string cardNo);

        dto_Patients GetPatientById(long patientId);

        dto_Patients GetPatientByCardNumber(string cardNumber);

        IQueryable<dto_Patients> GetAllPateints();
        
        IEnumerable<dto_Patients> GetAllPateintsE();

        bool AddPatient(dto_Patients pat);

        bool EditPatient(dto_Patients pat);

        bool DeletePatient(long patId);
    }
}
