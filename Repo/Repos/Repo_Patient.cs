using AutoMapper;
using AutoMapper.QueryableExtensions;
using Repo.DTOs;
using Repo.Models;
using System;
using System.Linq;

namespace Repo.Repos
{
    public class Repo_Patient : RepositoryBase<PamoDbEntities, patient>, IRepo_Patient
    {
        public bool CardNumberExist(string cardNo)
        {
            var crdNo = cardNo.Trim();
            if (string.IsNullOrEmpty(crdNo))
                return false;
            var getCardNo = _entities.patients.FirstOrDefault(n => n.CardNumber == crdNo);
            if (getCardNo != null)
                return true;
            else
                return false;
        }

        public dto_Patients GetPatientById(int patientId)
        {
            var result = _entities.patients.ProjectTo<dto_Patients>().FirstOrDefault(n => n.ID == patientId);
            return result;
        }

        public dto_Patients GetPatientByCardNumber(string cardNumber)
        {
            var crdNo = cardNumber.Trim();
            if (string.IsNullOrEmpty(crdNo))
                return null;
            var result = _entities.patients.ProjectTo<dto_Patients>().FirstOrDefault(n => n.CardNumber == crdNo);
            return result;
        }

        public IQueryable<dto_Patients> GetAllPatients()
        {
            var allPatients = _entities.patients.ProjectTo<dto_Patients>();
            return allPatients;
        }

        public RepositoryActionResult<dto_Patients> AddPatient(dto_Patients pat)
        {            
            try
            {
                var entity = Mapper.Map<patient>(pat);
                _entities.patients.Add(entity);
                var result = _entities.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<dto_Patients>(pat, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<dto_Patients>(pat, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<dto_Patients>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<dto_Patients> EditPatient(dto_Patients pat)
        {            
            try
            {
                var getPatient = _entities.patients.FirstOrDefault(n => n.ID == pat.ID);
                if (getPatient == null)
                    return new RepositoryActionResult<dto_Patients>(pat, RepositoryActionStatus.NothingModified, null);
                var entity = Mapper.Map<patient>(pat);
                _entities.Set<patient>().Attach(entity);
                _entities.Entry<patient>(entity).State = System.Data.Entity.EntityState.Modified;
                var result = _entities.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<dto_Patients>(pat, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<dto_Patients>(pat, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<dto_Patients>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<dto_Patients> DeletePatient(long patId)
        {
            try
            {
                var patient = _entities.patients.FirstOrDefault(n => n.ID == patId);
                if (patient == null)
                    return new RepositoryActionResult<dto_Patients>(null, RepositoryActionStatus.NothingModified);
                _entities.patients.Remove(patient);
                var result = _entities.SaveChanges();
                if (result > 0)
                    return new RepositoryActionResult<dto_Patients>(null, RepositoryActionStatus.Deleted);
                else
                    return new RepositoryActionResult<dto_Patients>(null, RepositoryActionStatus.NothingModified);                
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<dto_Patients>(null, RepositoryActionStatus.Error, ex);
            }
        }

        #region Patient's Biovital Repo
        public IQueryable<dto_BioVital> GetAllBiovitalsForPatient(int patientID)
        {
            var allBio = _entities.biovitals.ProjectTo<dto_BioVital>().Where(n => n.PatientID == patientID);
            return allBio;
        }

        public IQueryable<dto_BioVital> Get5BiovitalsForPatient(int patientID)
        {
            var allBio = _entities.biovitals.ProjectTo<dto_BioVital>()
                .Where(n => n.PatientID == patientID)
                .OrderByDescending(n => n.C_Date)
                .Take(5);
            return allBio;
        }

        public RepositoryActionResult<dto_BioVital> AddBiovital4Patient(dto_BioVital bio, int patientID)
        {
            try
            {
                bio.PatientID = patientID;
                var entity = Mapper.Map<biovital>(bio);
                _entities.biovitals.Add(entity);
                var result = _entities.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<dto_BioVital>(bio, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<dto_BioVital>(bio, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<dto_BioVital>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<dto_BioVital> EditBiovital4Patient(dto_BioVital bio)
        {
            try
            {
                var getBio = _entities.biovitals.FirstOrDefault(n => n.ID == bio.ID);
                if (getBio == null)
                    return new RepositoryActionResult<dto_BioVital>(bio, RepositoryActionStatus.NothingModified, null);
                var entity = Mapper.Map<biovital>(bio);
                _entities.Set<biovital>().Attach(entity);
                _entities.Entry<biovital>(entity).State = System.Data.Entity.EntityState.Modified;
                var result = _entities.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<dto_BioVital>(bio, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<dto_BioVital>(bio, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<dto_BioVital>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<dto_BioVital> DeleteBiovital4Patient(int bioId)
        {
            try
            {
                var biovit = _entities.biovitals.FirstOrDefault(n => n.ID == bioId);
                if (biovit == null)
                    return new RepositoryActionResult<dto_BioVital>(null, RepositoryActionStatus.NothingModified);
                _entities.biovitals.Remove(biovit);
                var result = _entities.SaveChanges();
                if (result > 0)
                    return new RepositoryActionResult<dto_BioVital>(null, RepositoryActionStatus.Deleted);
                else
                    return new RepositoryActionResult<dto_BioVital>(null, RepositoryActionStatus.NothingModified);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<dto_BioVital>(null, RepositoryActionStatus.Error, ex);
            }
        }
        #endregion

        //#region Appointment Repo

        //public IQueryable<appointment> GetAllAppointments()
        //{
        //    var model = _entities.appointments;
        //    return model;
        //}

        //public appointment GetAnAppointment(long id)
        //{
        //    var model = _entities.appointments.FirstOrDefault(n => n.ID == id);
        //    return model;
        //}

        //public IQueryable<appointment> GetPatientAppointment(string cardNumber)
        //{
        //    var model = _entities.appointments.Where(n => n.Card_Number == cardNumber);
        //    return model;
        //}

        //public bool AddPatientsAppointment(appointment patApp, string cardNumber, long AttdID)
        //{
        //    if (cardNumber == null || AttdID < 1)
        //        return false;
        //    patApp.Card_Number = cardNumber;
        //    patApp.AttID = AttdID;
        //    var entity = Mapper.Map<appointment>(patApp);
        //    _entities.appointments.Add(entity);
        //    var result = _entities.SaveChanges();
        //    if (result > 0)
        //        return true;
        //    else
        //        return false;
        //}

        //public bool EditPatientsAppointment(appointment patApp, string cardNumber, long AttdID)
        //{
        //    if (cardNumber == null || AttdID < 1)
        //        return false;
        //    var getAppointment = _entities.appointments.FirstOrDefault(n => n.Card_Number == cardNumber & n.AttID == AttdID);
        //    if (getAppointment == null)
        //        return false;
        //    var entity = Mapper.Map<appointment>(getAppointment);
        //    _entities.Set<appointment>().Attach(entity);
        //    _entities.Entry<appointment>(entity).State = System.Data.Entity.EntityState.Modified;
        //    var result = _entities.SaveChanges();
        //    if (result > 0)
        //        return true;
        //    else
        //        return false;
        //}

        //public bool DeletePatientsAppointment(long ID)
        //{
        //    var appointment = _entities.appointments.FirstOrDefault(n => n.ID == ID);
        //    if (appointment == null)
        //        return false;
        //    _entities.appointments.Remove(appointment);
        //    var result = _entities.SaveChanges();
        //    if (result > 0)
        //        return true;
        //    else
        //        return false;

        //}
        //#endregion
    }
}
