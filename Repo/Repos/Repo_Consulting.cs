using AutoMapper;
using AutoMapper.QueryableExtensions;
using Repo.DTOs;
using Repo.Models;
using System;
using System.Data.Entity;
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

        #region Patient's Prescription Repo
        public IQueryable<dto_Prescription> GetPrescriptionForAttendance(int attdID)
        {
            var allPres = _entities.prescriptions.ProjectTo<dto_Prescription>().Where(n => n.AttID == attdID);
            return allPres;
        }
        
        public RepositoryActionResult<dto_Prescription> AddPrescription(dto_Prescription bio, int attdID)
        {
            try
            {
                bio.AttID = attdID;
                var entity = Mapper.Map<prescription>(bio);
                _entities.prescriptions.Add(entity);
                var result = _entities.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<dto_Prescription>(bio, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<dto_Prescription>(bio, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<dto_Prescription>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<dto_Prescription> EditPrescription(dto_Prescription bio)
        {
            try
            {
                var getBio = _entities.prescriptions.FirstOrDefault(n => n.ID == bio.ID);
                if (getBio == null)
                    return new RepositoryActionResult<dto_Prescription>(bio, RepositoryActionStatus.NothingModified, null);
                var entity = Mapper.Map<prescription>(bio);
                _entities.Set<prescription>().Attach(entity);
                _entities.Entry<prescription>(entity).State = System.Data.Entity.EntityState.Modified;
                var result = _entities.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<dto_Prescription>(bio, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<dto_Prescription>(bio, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<dto_Prescription>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<dto_Prescription> UpdatePrescriptionByPahrm(dto_Prescription bio)
        {
            try
            {
                var getBio = _entities.prescriptions.FirstOrDefault(n => n.ID == bio.ID);
                if (getBio == null)
                    return new RepositoryActionResult<dto_Prescription>(bio, RepositoryActionStatus.NothingModified, null);
                //define fields updated by pharmacy/chemist
                getBio.Dose = bio.Dose;
                getBio.Duration = bio.Duration;
                getBio.Qty = bio.Qty;
                getBio.DispensedOn = bio.DispensedOn;
                getBio.DispensedByID = bio.DispensedByID;
                getBio.DispensedBy = bio.DispensedBy;
                getBio.Comment = bio.Comment;
                var result = _entities.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<dto_Prescription>(bio, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<dto_Prescription>(bio, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<dto_Prescription>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<dto_Prescription> DeletePrescription(int bioId)
        {
            try
            {
                var biovit = _entities.prescriptions.FirstOrDefault(n => n.ID == bioId);
                if (biovit == null)
                    return new RepositoryActionResult<dto_Prescription>(null, RepositoryActionStatus.NothingModified);
                _entities.prescriptions.Remove(biovit);
                var result = _entities.SaveChanges();
                if (result > 0)
                    return new RepositoryActionResult<dto_Prescription>(null, RepositoryActionStatus.Deleted);
                else
                    return new RepositoryActionResult<dto_Prescription>(null, RepositoryActionStatus.NothingModified);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<dto_Prescription>(null, RepositoryActionStatus.Error, ex);
            }
        }
        #endregion

        #region LAB Repo
        public IQueryable<dto_LabRequest> GetAllLabRequests()
        {
            var model = _entities.labrequests.ProjectTo<dto_LabRequest>();
            return model;
        }

        public IQueryable<dto_LabRequest> GetLabRequestForAttendance(int attdID)
        {
            var allPres = _entities.labrequests.ProjectTo<dto_LabRequest>().Where(n => n.AttID == attdID);
            return allPres;
        }

        public RepositoryActionResult<dto_LabRequest> AddLab(dto_LabRequest pat, int attdId)
        {
            try
            {
                pat.AttID = attdId;
                var entity = Mapper.Map<labrequest>(pat);
                _entities.labrequests.Add(entity);
                var result = _entities.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<dto_LabRequest>(pat, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<dto_LabRequest>(pat, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<dto_LabRequest>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<dto_LabRequest> EditLab(dto_LabRequest pat)
        {
            try
            {
                var getBio = _entities.labrequests.FirstOrDefault(n => n.ID == pat.ID);
                if (getBio == null)
                    return new RepositoryActionResult<dto_LabRequest>(pat, RepositoryActionStatus.NothingModified, null);
                var entity = Mapper.Map<labrequest>(pat);
                var local = _entities.Set<labrequest>().Local.FirstOrDefault(f => f.ID == pat.ID);
                if (local != null)
                {
                    _entities.Entry(local).State = EntityState.Detached;
                }
                _entities.Set<labrequest>().Attach(entity);
                _entities.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                var result = _entities.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<dto_LabRequest>(pat, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<dto_LabRequest>(pat, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<dto_LabRequest>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<dto_LabRequest> DeleteLab(int labId)
        {
            try
            {
                var biovit = _entities.labrequests.FirstOrDefault(n => n.ID == labId);
                if (biovit == null)
                    return new RepositoryActionResult<dto_LabRequest>(null, RepositoryActionStatus.NothingModified);
                if (string.IsNullOrEmpty(biovit.Remark))
                    return new RepositoryActionResult<dto_LabRequest>(null, RepositoryActionStatus.NothingModified, "You cannot delete a Lab request with valid Lab report.", "");
                _entities.labrequests.Remove(biovit);
                var result = _entities.SaveChanges();
                if (result > 0)
                    return new RepositoryActionResult<dto_LabRequest>(null, RepositoryActionStatus.Deleted);
                else
                    return new RepositoryActionResult<dto_LabRequest>(null, RepositoryActionStatus.NothingModified);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<dto_LabRequest>(null, RepositoryActionStatus.Error, ex);
            }
        }
        #endregion
    }
}
