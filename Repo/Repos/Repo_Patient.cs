using AutoMapper;
using AutoMapper.QueryableExtensions;
using Repo.DTOs;
using Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Repos
{
    public class Repo_Patient : RepositoryBase<PamoDbEntities, eregister>, IRepo_Patient
    {
        public bool CardNumberExist(string cardNo)
        {
            var crdNo = cardNo.Trim();
            if (string.IsNullOrEmpty(crdNo))
                return false;
            var getCardNo = _entities.eregisters.FirstOrDefault(n => n.Card_Number == crdNo);
            if (getCardNo != null)
                return true;
            else
                return false;
        }

        public dto_Patients GetPatientById(long patientId)
        {
            var result = _entities.eregisters.ProjectTo<dto_Patients>().FirstOrDefault(n => n.ID == patientId);
            return result;
        }

        public dto_Patients GetPatientByCardNumber(string cardNumber)
        {
            var crdNo = cardNumber.Trim();
            if (string.IsNullOrEmpty(crdNo))
                return null;
            var result = _entities.eregisters.ProjectTo<dto_Patients>().FirstOrDefault(n => n.Card_Number == crdNo);
            return result;
        }

        public IQueryable<dto_Patients> GetAllPateints()
        {
            var allPatients = _entities.eregisters.ProjectTo<dto_Patients>();
            return allPatients;
        }
                
        public IEnumerable<dto_Patients> GetAllPateintsE()
        {
            var allPatients = _entities.eregisters.ProjectTo<dto_Patients>();
            return allPatients;
        }

        public bool AddPatient(dto_Patients pat)
        {
            var entity = Mapper.Map<eregister>(pat);
            _entities.eregisters.Add(entity);
            var result = _entities.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }

        public bool EditPatient(dto_Patients pat)
        {
            var entity = Mapper.Map<eregister>(pat);
            _entities.Set<eregister>().Attach(entity);
            _entities.Entry<eregister>(entity).State = System.Data.Entity.EntityState.Modified;
            var result = _entities.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }

        public bool DeletePatient(long patId)
        {
            var patient = _entities.eregisters.FirstOrDefault(n => n.ID == patId);
            if (patient != null)
                return false;
            _entities.eregisters.Remove(patient);
            var result = _entities.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;

        }
    }
}
