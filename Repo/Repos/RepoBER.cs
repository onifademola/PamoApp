using Repo.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Repo.Repos
{
    //BER - BACK END REPAIR controls
    public class RepoBER : RepositoryBase<PamoDbEntities, User>, IRepoBER
    {
        public static readonly int _batchSize = 1000;

        public bool GenerateUserUniqueID()
        {
            try
            {
                var appUsers = _entities.Users.Where(n => n.Id == null);
                foreach (var item in appUsers)
                {
                    Guid nguid = Guid.NewGuid();
                    item.Id = nguid.ToString();
                }
                try
                {
                    var result = _entities.SaveChanges();
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    var msg = ex;
                    return false;
                }
            }
            catch (System.Exception)
            {
                //something went wrong
                return false;
            }
        }

        public bool FixPatientAttendanceTB()
        {
            try
            {
                int result = 0;
                var getAllAttd = _entities.attendances.Where(n => n.PatientID == null && n.CardNumber != null).ToList();
                int DataCount = getAllAttd.Count;
                int BatchSize = _batchSize;
                int numberOfPages = (DataCount / BatchSize) + (DataCount % BatchSize == 0 ? 0 : 1);
                for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
                {                    
                    var nGetAllAttd = getAllAttd.Skip(pageIndex * BatchSize).Take(BatchSize);
                    foreach (var item in nGetAllAttd)
                    {
                        var getPatient = _entities.patients.FirstOrDefault(n => n.CardNumber == item.CardNumber);
                        if (getPatient != null)
                            item.PatientID = getPatient.ID;
                    }
                    int saveResult = _entities.SaveChanges();
                    result = result + saveResult;
                }                
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                var msg = ex;
                return false;
            }
        }

        public bool FixDoctorAttendanceTB()
        {
            try
            {
                int result = 0;
                var getAllAttd = _entities.attendances.Where(n => n.DoctorID == null).ToList();
                int DataCount = getAllAttd.Count;
                int BatchSize = _batchSize;
                int numberOfPages = (DataCount / BatchSize) + (DataCount % BatchSize == 0 ? 0 : 1);
                for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
                {
                    var nGetAllAttd = getAllAttd.Skip(pageIndex * BatchSize).Take(BatchSize);
                    foreach (var item in nGetAllAttd)
                    {
                        var getDoc = _entities.Users.FirstOrDefault(n => n.username == item.Consultant);
                        if (getDoc != null)
                            item.DoctorID = getDoc.user_id;
                    }
                    int saveResult = _entities.SaveChanges();
                    result = result + saveResult;
                }
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                var msg = ex;
                return false;
            }
        }

        public bool FixDoctorProcedurTB()
        {
            try
            {
                int result = 0;
                var getAllProced = _entities.procedurs.Where(n => n.DoctorID == null).ToList();
                int DataCount = getAllProced.Count;
                int BatchSize = _batchSize;
                int numberOfPages = (DataCount / BatchSize) + (DataCount % BatchSize == 0 ? 0 : 1);
                for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
                {
                    var nGetAllProced = getAllProced.Skip(pageIndex * BatchSize).Take(BatchSize);
                    foreach (var item in nGetAllProced)
                    {
                        var getDoc = _entities.Users.FirstOrDefault(n => n.username == item.Doctor);
                        if (getDoc != null)
                        item.DoctorID = getDoc.user_id;
                    }
                    int saveResult = _entities.SaveChanges();
                    result = result + saveResult;
                }
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                var msg = ex;
                return false;
            }
        }

        public bool FixPatientProcedurTB()
        {
            try
            {
                int result = 0;
                var getAllProced = _entities.procedurs.Where(n => n.PatientID == null).ToList();
                int DataCount = getAllProced.Count;
                int BatchSize = _batchSize;
                int numberOfPages = (DataCount / BatchSize) + (DataCount % BatchSize == 0 ? 0 : 1);
                for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
                {
                    var nGetAllProced = getAllProced.Skip(pageIndex * BatchSize).Take(BatchSize);
                    foreach (var item in nGetAllProced)
                    {
                        var getPat = _entities.patients.FirstOrDefault(n => n.CardNumber == item.CardNumber);
                        if (getPat != null)
                        item.PatientID = getPat.ID;
                    }
                    int saveResult = _entities.SaveChanges();
                    result = result + saveResult;
                }
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                var msg = ex;
                return false;
            }
        }

        public bool FixPatientPhyExamTB()
        {
            try
            {
                int result = 0;
                var getAllPhyExams = _entities.phyexams.Where(n => n.PatientID == null).ToList();
                int DataCount = getAllPhyExams.Count;
                int BatchSize = _batchSize;
                int numberOfPages = (DataCount / BatchSize) + (DataCount % BatchSize == 0 ? 0 : 1);
                for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
                {
                    var nGetAllPhyExams = getAllPhyExams.Skip(pageIndex * BatchSize).Take(BatchSize);
                    foreach (var item in nGetAllPhyExams)
                    {
                        var getPat = _entities.patients.FirstOrDefault(n => n.CardNumber == item.CardNumber);
                        if (getPat != null)
                        item.PatientID = getPat.ID;
                    }
                    int saveResult = _entities.SaveChanges();
                    result = result + saveResult;
                }
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                var msg = ex;
                return false;
            }
        }

        //public bool FixPatientRoundsTB()
        //{
        //    try
        //    {
        //        int result = 0;
        //        var getAllRounds = _entities.rounds.Where(n => n.PatientID == null).ToList();
        //        int DataCount = getAllRounds.Count;
        //        int BatchSize = _batchSize;
        //        int numberOfPages = (DataCount / BatchSize) + (DataCount % BatchSize == 0 ? 0 : 1);
        //        for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
        //        {
        //            var nGetAllRounds = getAllRounds.Skip(pageIndex * BatchSize).Take(BatchSize);
        //            foreach (var item in nGetAllRounds)
        //            {
        //                var getPat = _entities.patients.FirstOrDefault(n => n.CardNumber == item.CardNumber);
        //                if (getPat != null)
        //                item.PatientID = getPat.ID;
        //            }
        //            int saveResult = _entities.SaveChanges();
        //            result = result + saveResult;
        //        }
        //        if (result > 0)
        //            return true;
        //        else
        //            return false;                
        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex;
        //        return false;
        //    }
        //}

        public bool FixDoctorPhyExamTB()
        {
            try
            {
                int result = 0;
                var getAllPhyExams = _entities.phyexams.Where(n => n.DoctorId == null).ToList();
                int DataCount = getAllPhyExams.Count;
                int BatchSize = _batchSize;
                int numberOfPages = (DataCount / BatchSize) + (DataCount % BatchSize == 0 ? 0 : 1);
                for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
                {
                    var nGetAllPhyExams = getAllPhyExams.Skip(pageIndex * BatchSize).Take(BatchSize);
                    foreach (var item in nGetAllPhyExams)
                    {
                        var getDoc = _entities.Users.FirstOrDefault(n => n.username == item.Doctor);
                        if (getDoc != null)
                            item.DoctorId = getDoc.user_id;
                    }
                    int saveResult = _entities.SaveChanges();
                    result = result + saveResult;
                }
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                var msg = ex;
                return false;
            }
        }

        //public bool FixDoctorRoundsTB()
        //{
        //    try
        //    {
        //        int result = 0;
        //        var getAllRounds = _entities.rounds.Where(n => n.DoctorID == null).ToList();
        //        int DataCount = getAllRounds.Count;
        //        int BatchSize = _batchSize;
        //        int numberOfPages = (DataCount / BatchSize) + (DataCount % BatchSize == 0 ? 0 : 1);
        //        for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
        //        {
        //            var nGetAllRounds = getAllRounds.Skip(pageIndex * BatchSize).Take(BatchSize);
        //            foreach (var item in nGetAllRounds)
        //            {
        //                var getDoc = _entities.Users.FirstOrDefault(n => n.username == item.Doctor);
        //                if (getDoc != null)
        //                    item.DoctorID = getDoc.user_id;
        //            }
        //            int saveResult = _entities.SaveChanges();
        //            result = result + saveResult;
        //        }
        //        if (result > 0)
        //            return true;
        //        else
        //            return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex;
        //        return false;
        //    }
        //}

        public bool FixPatientPComplainTB()
        {
            try
            {
                int result = 0;
                var getAllComplains = _entities.PatientComplaints.Where(n => n.PatientID == null).ToList();
                int DataCount = getAllComplains.Count;
                int BatchSize = _batchSize;
                int numberOfPages = (DataCount / BatchSize) + (DataCount % BatchSize == 0 ? 0 : 1);
                for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
                {
                    var nGetAllComplains = getAllComplains.Skip(pageIndex * BatchSize).Take(BatchSize);
                    foreach (var item in nGetAllComplains)
                    {
                        var getPat = _entities.patients.FirstOrDefault(n => n.CardNumber == item.CardNumber);
                        if (getPat != null)
                            item.PatientID = getPat.ID;
                    }
                    int saveResult = _entities.SaveChanges();
                    result = result + saveResult;
                }
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                var msg = ex;
                return false;
            }
        }

        public bool FixPatientBiovitalsTB()
        {
            try
            {
                int result = 0;
                var getAllBiovitals = _entities.biovitals.Where(n => n.PatientID == null).ToList();
                int DataCount = getAllBiovitals.Count;
                int BatchSize = _batchSize;
                int numberOfPages = (DataCount / BatchSize) + (DataCount % BatchSize == 0 ? 0 : 1);
                for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
                {
                    var nGetAllBiovitals = getAllBiovitals.Skip(pageIndex * BatchSize).Take(BatchSize);
                    foreach (var item in nGetAllBiovitals)
                    {
                        var getPat = _entities.patients.FirstOrDefault(n => n.CardNumber == item.CardNumber);
                        if (getPat != null)
                            item.PatientID = getPat.ID;
                    }
                    int saveResult = _entities.SaveChanges();
                    result = result + saveResult;
                }
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                var msg = ex;
                return false;
            }
        }

        public bool FixPatientDeliveryTB()
        {
            try
            {
                int result = 0;
                var getAllDelivery = _entities.deliveries.Where(n => n.PatientID == null).ToList();
                int DataCount = getAllDelivery.Count;
                int BatchSize = _batchSize;
                int numberOfPages = (DataCount / BatchSize) + (DataCount % BatchSize == 0 ? 0 : 1);
                for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
                {
                    var nGetAllDelivery = getAllDelivery.Skip(pageIndex * BatchSize).Take(BatchSize);
                    foreach (var item in nGetAllDelivery)
                    {
                        var getPat = _entities.patients.FirstOrDefault(n => n.CardNumber == item.CardNumber);
                        if (getPat != null)
                            item.PatientID = getPat.ID;
                    }
                    int saveResult = _entities.SaveChanges();
                    result = result + saveResult;
                }
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                var msg = ex;
                return false;
            }
        }

        public bool FixPatientDiagnosisTB()
        {
            try
            {
                int result = 0;
                var getAllDiagnosis = _entities.diagnosis.Where(n => n.PatientID == null).ToList();
                int DataCount = getAllDiagnosis.Count;
                int BatchSize = _batchSize;
                int numberOfPages = (DataCount / BatchSize) + (DataCount % BatchSize == 0 ? 0 : 1);
                for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
                {
                    var nGetAllDiagnosis = getAllDiagnosis.Skip(pageIndex * BatchSize).Take(BatchSize);
                    foreach (var item in nGetAllDiagnosis)
                    {
                        var getPat = _entities.patients.FirstOrDefault(n => n.CardNumber == item.CardNumber);
                        if (getPat != null)
                            item.PatientID = getPat.ID;
                    }
                    int saveResult = _entities.SaveChanges();
                    result = result + saveResult;
                }
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                var msg = ex;
                return false;
            }
        }

        public bool FixPatientInvestigationTB()
        {
            try
            {
                int result = 0;
                var getAllInvestigatn = _entities.investigatns.Where(n => n.PatientID == null).ToList();
                int DataCount = getAllInvestigatn.Count;
                int BatchSize = _batchSize;
                int numberOfPages = (DataCount / BatchSize) + (DataCount % BatchSize == 0 ? 0 : 1);
                for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
                {
                    var nGetAllInvestigatn = getAllInvestigatn.Skip(pageIndex * BatchSize).Take(BatchSize);
                    foreach (var item in nGetAllInvestigatn)
                    {
                        var getPat = _entities.patients.FirstOrDefault(n => n.CardNumber == item.CardNumber);
                        if (getPat != null)
                            item.PatientID = getPat.ID;
                    }
                    int saveResult = _entities.SaveChanges();
                    result = result + saveResult;
                }
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                var msg = ex;
                return false;
            }
        }

        public bool FixPatientNursenoteTB()
        {
            try
            {
                int result = 0;
                var getAllNursenotes = _entities.NurseNotes.Where(n => n.PatientID == null).ToList();
                int DataCount = getAllNursenotes.Count;
                int BatchSize = _batchSize;
                int numberOfPages = (DataCount / BatchSize) + (DataCount % BatchSize == 0 ? 0 : 1);
                for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
                {
                    var nGetAllNursenotes = getAllNursenotes.Skip(pageIndex * BatchSize).Take(BatchSize);
                    foreach (var item in nGetAllNursenotes)
                    {
                        var getPat = _entities.patients.FirstOrDefault(n => n.CardNumber == item.CardNumber);
                        if (getPat != null)
                            item.PatientID = getPat.ID;
                    }
                    int saveResult = _entities.SaveChanges();
                    result = result + saveResult;
                }
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                var msg = ex;
                return false;
            }
        }

        public bool FixPatientScanReportTB()
        {
            try
            {
                int result = 0;
                var getAllScanReports = _entities.scanreports.Where(n => n.PatientID == null).ToList();
                int DataCount = getAllScanReports.Count;
                int BatchSize = _batchSize;
                int numberOfPages = (DataCount / BatchSize) + (DataCount % BatchSize == 0 ? 0 : 1);
                for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
                {
                    var nGetAllScanReports = getAllScanReports.Skip(pageIndex * BatchSize).Take(BatchSize);
                    foreach (var item in nGetAllScanReports)
                    {
                        var getPat = _entities.patients.FirstOrDefault(n => n.CardNumber == item.CardNumber);
                        if (getPat != null)
                            item.PatientID = getPat.ID;
                    }
                    int saveResult = _entities.SaveChanges();
                    result = result + saveResult;
                }
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                var msg = ex;
                return false;
            }
        }

        public bool FixPatientSurgeryTB()
        {
            try
            {
                int result = 0;
                var getAllSurgeries = _entities.surgeries.Where(n => n.PatientID == null).ToList();
                int DataCount = getAllSurgeries.Count;
                int BatchSize = _batchSize;
                int numberOfPages = (DataCount / BatchSize) + (DataCount % BatchSize == 0 ? 0 : 1);
                for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
                {
                    var nGetAllSurgeries = getAllSurgeries.Skip(pageIndex * BatchSize).Take(BatchSize);
                    foreach (var item in nGetAllSurgeries)
                    {
                        var getPat = _entities.patients.FirstOrDefault(n => n.CardNumber == item.CardNumber);
                        if (getPat != null)
                            item.PatientID = getPat.ID;
                    }
                    int saveResult = _entities.SaveChanges();
                    result = result + saveResult;
                }
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                var msg = ex;
                return false;
            }
        }

        public bool FixPatientTblRecTB()
        {
            try
            {
                int result = 0;
                var getAllTblRecs = _entities.tbl_rec.Where(n => n.PatientID == null).ToList();
                int DataCount = getAllTblRecs.Count;
                int BatchSize = _batchSize;
                int numberOfPages = (DataCount / BatchSize) + (DataCount % BatchSize == 0 ? 0 : 1);
                for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
                {
                    var nGetAllTblRecs = getAllTblRecs.Skip(pageIndex * BatchSize).Take(BatchSize);
                    foreach (var item in nGetAllTblRecs)
                    {
                        var getPat = _entities.patients.FirstOrDefault(n => n.CardNumber == item.Card);
                        if (getPat != null)
                            item.PatientID = getPat.ID;
                    }
                    int saveResult = _entities.SaveChanges();
                    result = result + saveResult;
                }
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                var msg = ex;
                return false;
            }
        }

        public bool FixPatientWardTB()
        {
            try
            {
                int result = 0;
                var getAllWards = _entities.Admissions.Where(n => n.PatientID == null).ToList();
                int DataCount = getAllWards.Count;
                int BatchSize = _batchSize;
                int numberOfPages = (DataCount / BatchSize) + (DataCount % BatchSize == 0 ? 0 : 1);
                for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
                {
                    var nGetAllWards = getAllWards.Skip(pageIndex * BatchSize).Take(BatchSize);
                    foreach (var item in nGetAllWards)
                    {
                        var getPat = _entities.patients.FirstOrDefault(n => n.CardNumber == item.CardNumber);
                        if (getPat != null)
                            item.PatientID = getPat.ID;
                    }
                    int saveResult = _entities.SaveChanges();
                    result = result + saveResult;
                }
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                var msg = ex;
                return false;
            }
        }

        public bool FixPatientAppointmentTB()
        {
            try
            {
                int result = 0;
                var getAllAppointments = _entities.appointments.Where(n => n.PatientID == null).ToList();
                int DataCount = getAllAppointments.Count;
                int BatchSize = _batchSize;
                int numberOfPages = (DataCount / BatchSize) + (DataCount % BatchSize == 0 ? 0 : 1);
                for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
                {
                    var nGetAllAppointments = getAllAppointments.Skip(pageIndex * BatchSize).Take(BatchSize);
                    foreach (var item in nGetAllAppointments)
                    {
                        var getPat = _entities.patients.FirstOrDefault(n => n.CardNumber == item.CardNumber);
                        if (getPat != null)
                            item.PatientID = getPat.ID;
                    }
                    int saveResult = _entities.SaveChanges();
                    result = result + saveResult;
                }
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                var msg = ex;
                return false;
            }
        }

        public bool FixDoctorAppointmentTB()
        {
            try
            {
                int result = 0;
                var getAllAppointments = _entities.appointments.Where(n => n.DoctorID == null).ToList();
                int DataCount = getAllAppointments.Count;
                int BatchSize = _batchSize;
                int numberOfPages = (DataCount / BatchSize) + (DataCount % BatchSize == 0 ? 0 : 1);
                for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
                {
                    var nGetAllAppointments = getAllAppointments.Skip(pageIndex * BatchSize).Take(BatchSize);
                    foreach (var item in nGetAllAppointments)
                    {
                        var getDoc = _entities.Users.FirstOrDefault(n => n.username == item.Doctor);
                        if (getDoc != null)
                            item.DoctorID = getDoc.user_id;
                    }
                    int saveResult = _entities.SaveChanges();
                    result = result + saveResult;
                }
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                var msg = ex;
                return false;
            }
        }

        public bool FixNurseNursenoteTB()
        {
            try
            {
                int result = 0;
                var getAllNursenotes = _entities.NurseNotes.Where(n => n.NurseID == null).ToList();
                int DataCount = getAllNursenotes.Count;
                int BatchSize = _batchSize;
                int numberOfPages = (DataCount / BatchSize) + (DataCount % BatchSize == 0 ? 0 : 1);
                for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
                {
                    var nGetAllNursenotes = getAllNursenotes.Skip(pageIndex * BatchSize).Take(BatchSize);
                    foreach (var item in nGetAllNursenotes)
                    {
                        var getDoc = _entities.Users.FirstOrDefault(n => n.username == item.Nurse);
                        if (getDoc != null)
                            item.NurseID = getDoc.user_id;
                    }
                    int saveResult = _entities.SaveChanges();
                    result = result + saveResult;
                }
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                var msg = ex;
                return false;
            }
        }

        public bool FixComplainUserTB()
        {
            try
            {
                int result = 0;
                var getAllPatientComps = _entities.PatientComplaints.Where(n => n.TakenByID == null).ToList();
                int DataCount = getAllPatientComps.Count;
                int BatchSize = _batchSize;
                int numberOfPages = (DataCount / BatchSize) + (DataCount % BatchSize == 0 ? 0 : 1);
                for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
                {
                    var nGetAllPatientComps = getAllPatientComps.Skip(pageIndex * BatchSize).Take(BatchSize);
                    foreach (var item in nGetAllPatientComps)
                    {
                        var getDoc = _entities.Users.FirstOrDefault(n => n.username == item.TakenBy);
                        if (getDoc != null)
                            item.TakenByID = getDoc.user_id;
                    }
                    int saveResult = _entities.SaveChanges();
                    result = result + saveResult;
                }
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                var msg = ex;
                return false;
            }
        }

        public bool FixAttendanceConsultingRoomTB()
        {
            try
            {
                int result = 0;
                var getAllAttd = _entities.attendances.Where(n => n.ConsultingRoomID == null).ToList();
                int DataCount = getAllAttd.Count;
                int BatchSize = _batchSize;
                int numberOfPages = (DataCount / BatchSize) + (DataCount % BatchSize == 0 ? 0 : 1);
                for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
                {
                    var nGetAllAttd = getAllAttd.Skip(pageIndex * BatchSize).Take(BatchSize);
                    foreach (var item in nGetAllAttd)
                    {
                        var getCons = _entities.consultingRooms.FirstOrDefault(n => n.Room == item.Room);
                        if (getCons != null)
                            item.ConsultingRoomID = getCons.ID;
                    }
                    int saveResult = _entities.SaveChanges();
                    result = result + saveResult;
                }
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                var msg = ex;
                return false;
            }
        }

        public bool FixDiagnosisDiagnosedBy()
        {
            try
            {
                int result = 0;
                var getAllDiagnosis = _entities.diagnosis.Where(n => n.DiagnosedByID == null).ToList();
                int DataCount = getAllDiagnosis.Count;
                int BatchSize = _batchSize;
                int numberOfPages = (DataCount / BatchSize) + (DataCount % BatchSize == 0 ? 0 : 1);
                for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
                {
                    var nGetAllDiagnosis = getAllDiagnosis.Skip(pageIndex * BatchSize).Take(BatchSize);
                    foreach (var item in nGetAllDiagnosis)
                    {
                        var getDoc = _entities.Users.FirstOrDefault(n => n.username == item.DiagnosedBy);
                        if (getDoc != null)
                            item.DiagnosedByID = getDoc.user_id;
                    }
                    int saveResult = _entities.SaveChanges();
                    result = result + saveResult;
                }
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                var msg = ex;
                return false;
            }
        }
    }
}
