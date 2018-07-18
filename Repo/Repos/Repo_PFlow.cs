using AutoMapper.QueryableExtensions;
using OfficeOnline.DataBlock;
using Repo.DTOs;
using Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Repos
{
    public class Repo_PFlow : RepositoryBase<PamoDbEntities, ProcessFlow>, IRepo_PFlow
    {
        Repo_Util _repoUtil = new Repo_Util();

        public AspNetUser GetUserDoingTask(string userASPIdentityID)
        {
            var user = _entities.AspNetUsers.FirstOrDefault(n => n.Id == userASPIdentityID);
            return user;
        }

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

        public bool AddNewFlowQueue(FlowQueue fq)
        {
            //call the datetime service here to reduce that data size transfered between server and client
            fq.LastTimeStamp = _repoUtil.GetNijaTime(DateTime.Now);
            _entities.FlowQueues.Add(fq);
            var result = _entities.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }

        public bool UpdateFlowQueue(FlowQueue fq, DateTime eventTime)
        {            
            _entities.Set<FlowQueue>().Attach(fq);
            _entities.Entry<FlowQueue>(fq).State = System.Data.Entity.EntityState.Modified;

            //update the previous process flow end time
            ProcessFlow lpf = _entities.ProcessFlows.Where(n => n.AttID == fq.AttID)
                .OrderByDescending(n => n.ProcessID).FirstOrDefault();
            lpf.EndTime = eventTime;
            lpf.TimeTaken = (lpf.EndTime.Value - lpf.StartTime.Value).Ticks;

            var result = _entities.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }

        public bool DeleteFlowQueue(int id)
        {
            FlowQueue fq = _entities.FlowQueues.FirstOrDefault(n => n.ID == id);
            _entities.FlowQueues.Remove(fq);
            var result = _entities.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }

        public bool GenerateProcessFlow(ProcessFlow pf)
        {
            pf.StartTime = _repoUtil.GetNijaTime(DateTime.Now);
            _entities.ProcessFlows.Add(pf);
            var result = _entities.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }

        #region Queue Flows

        public bool QueuePatientAtOPD(int AttID, AspNetUser userDoingTask) //Process Status 1 - At OPD
        {
            int processStatus = 1;
            DateTime eventTime = _repoUtil.GetNijaTime(DateTime.Now);
            
            //first generate a process flow
            ProcessFlow newPF = new ProcessFlow();
            newPF.AttID = AttID;
            newPF.Comment = "Patient sent to OPD by " + userDoingTask.Email;
            newPF.StatusID = processStatus;
            newPF.StartTime = eventTime;
            newPF.UserID = userDoingTask.user_id;
            _entities.ProcessFlows.Add(newPF);

            //then create the flow queue
            FlowQueue newFQ = new FlowQueue();
            newFQ.AttID = AttID;
            newFQ.CurrentStatusID = processStatus;
            newFQ.LastTimeStamp = eventTime;
            newFQ.LastUpdatedBy = userDoingTask.user_id;
            _entities.FlowQueues.Add(newFQ);

            var result = _entities.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }

        public bool QueuePatientAtOPDFromLab(int AttID, AspNetUser userDoingTask) //Process Status 1 - At OPD
        {
            int processStatus = 1;
            DateTime eventTime = _repoUtil.GetNijaTime(DateTime.Now);

            //first generate a process flow
            ProcessFlow newPF = new ProcessFlow();
            newPF.AttID = AttID;
            newPF.Comment = "Patient returned to OPD from Lab by " + userDoingTask.Email;
            newPF.StatusID = processStatus;
            newPF.StartTime = eventTime;
            newPF.UserID = userDoingTask.user_id;
            _entities.ProcessFlows.Add(newPF);

            //then create the flow queue
            FlowQueue newFQ = new FlowQueue();
            newFQ.AttID = AttID;
            newFQ.CurrentStatusID = processStatus;
            newFQ.LastTimeStamp = eventTime;
            newFQ.LastUpdatedBy = userDoingTask.user_id;
            _entities.FlowQueues.Add(newFQ);

            var result = _entities.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }

        public bool QueuePatientAtConsulting(int AttID, int consultingRoomID, AspNetUser userDoingTask) //Process Status 2 - At Consulting
        {
            int processStatus = 2;
            DateTime eventTime = _repoUtil.GetNijaTime(DateTime.Now);

            //first, update the existing the flow queue
            //get flow queue by AttID, since only an instance of Attendance can be in a flowqueue per visit
            FlowQueue eFQ = _entities.FlowQueues.FirstOrDefault(n => n.AttID == AttID);
            if (eFQ == null)
                return false;
            eFQ.CurrentStatusID = processStatus;
            eFQ.LastTimeStamp = eventTime;
            eFQ.LastUpdatedBy = userDoingTask.user_id;
            UpdateFlowQueue(eFQ, eventTime);
            _entities.SaveChanges();

            //then generate a new process flow
            //PamoDbEntities db = new PamoDbEntities();
            ProcessFlow newPF = new ProcessFlow();
            newPF.AttID = AttID;
            newPF.Comment = "Patient sent to Consulting by " + userDoingTask.Email;
            newPF.StatusID = processStatus;
            newPF.StartTime = eventTime;
            newPF.UserID = userDoingTask.user_id;
            _entities.ProcessFlows.Add(newPF);
            _entities.SaveChanges();

            //update attendance with consulting room id
            dto_Attendance attendance = _entities.attendances.ProjectTo<dto_Attendance>().FirstOrDefault(n => n.ID == AttID);
            if (attendance == null)
                return false;
            if (consultingRoomID > 0)
            attendance.ConsultingRoomID = consultingRoomID;
            UpdateAttendance(attendance);

            var result = 1;
            if (result > 0)
                return true;
            else
                return false;
        }

        public bool QueuePatientAtLab(int AttID, AspNetUser userDoingTask) //Process Status 3 - Awaiting Lab/Scan Result
        {
            int processStatus = 3;
            DateTime eventTime = _repoUtil.GetNijaTime(DateTime.Now);

            //first, update the existing the flow queue
            //get flow queue by AttID, since only an instance of Attendance can be in a flowqueue per visit
            FlowQueue eFQ = _entities.FlowQueues.FirstOrDefault(n => n.AttID == AttID);
            if (eFQ == null)
                return false;
            eFQ.CurrentStatusID = processStatus;
            eFQ.LastTimeStamp = eventTime;
            eFQ.LastUpdatedBy = userDoingTask.user_id;
            this.UpdateFlowQueue(eFQ, eventTime);

            //then generate a new process flow
            ProcessFlow newPF = new ProcessFlow();
            newPF.AttID = AttID;
            newPF.Comment = "Patient sent to the Lab by " + userDoingTask.Email;
            newPF.StatusID = processStatus;
            newPF.StartTime = eventTime;
            newPF.UserID = userDoingTask.user_id;
            _entities.ProcessFlows.Add(newPF);

            var result = _entities.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }

        public bool QueuePatientAtAdmission(int AttID, AspNetUser userDoingTask) //Process Status 4 - On Admission
        {
            int processStatus = 4;
            DateTime eventTime = _repoUtil.GetNijaTime(DateTime.Now);

            //first, update the existing the flow queue
            //get flow queue by AttID, since only an instance of Attendance can be in a flowqueue per visit
            FlowQueue eFQ = _entities.FlowQueues.FirstOrDefault(n => n.AttID == AttID);
            if (eFQ == null)
                return false;
            eFQ.CurrentStatusID = processStatus;
            eFQ.LastTimeStamp = eventTime;
            eFQ.LastUpdatedBy = userDoingTask.user_id;
            this.UpdateFlowQueue(eFQ, eventTime);

            //then generate a new process flow
            ProcessFlow newPF = new ProcessFlow();
            newPF.AttID = AttID;
            newPF.Comment = "Patient put on admission by " + userDoingTask.Email;
            newPF.StatusID = processStatus;
            newPF.StartTime = eventTime;
            newPF.UserID = userDoingTask.user_id;
            _entities.ProcessFlows.Add(newPF);

            var result = _entities.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }

        public bool QueuePatientAtPharmacy(int AttID, AspNetUser userDoingTask) //Process Status 5 - At Pharmacy
        {
            int processStatus = 5;
            DateTime eventTime = _repoUtil.GetNijaTime(DateTime.Now);

            //first, update the existing the flow queue
            //get flow queue by AttID, since only an instance of Attendance can be in a flowqueue per visit
            FlowQueue eFQ = _entities.FlowQueues.FirstOrDefault(n => n.AttID == AttID);
            if (eFQ == null)
                return false;
            eFQ.CurrentStatusID = processStatus;
            eFQ.LastTimeStamp = eventTime;
            eFQ.LastUpdatedBy = userDoingTask.user_id;
            this.UpdateFlowQueue(eFQ, eventTime);

            //then generate a new process flow
            ProcessFlow newPF = new ProcessFlow();
            newPF.AttID = AttID;
            newPF.Comment = "Patient sent to pharmacy by " + userDoingTask.Email;
            newPF.StatusID = processStatus;
            newPF.StartTime = eventTime;
            newPF.UserID = userDoingTask.user_id;
            _entities.ProcessFlows.Add(newPF);

            var result = _entities.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }

        public bool QueuePatientWithAccounts(int AttID, AspNetUser userDoingTask) //Process Status 6 - With Accounts
        {
            int processStatus = 6;
            DateTime eventTime = _repoUtil.GetNijaTime(DateTime.Now);

            //first, update the existing the flow queue
            //get flow queue by AttID, since only an instance of Attendance can be in a flowqueue per visit
            FlowQueue eFQ = _entities.FlowQueues.FirstOrDefault(n => n.AttID == AttID);
            if (eFQ == null)
                return false;
            eFQ.CurrentStatusID = processStatus;
            eFQ.LastTimeStamp = eventTime;
            eFQ.LastUpdatedBy = userDoingTask.user_id;
            this.UpdateFlowQueue(eFQ, eventTime);

            //then generate a new process flow
            ProcessFlow newPF = new ProcessFlow();
            newPF.AttID = AttID;
            newPF.Comment = "Patient sent to accounts by " + userDoingTask.Email;            
            newPF.StatusID = processStatus;
            newPF.StartTime = eventTime;
            newPF.UserID = userDoingTask.user_id;
            _entities.ProcessFlows.Add(newPF);

            var result = _entities.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }

        public bool QueueEnds(int AttID, AspNetUser userDoingTask) //Process Status 7 - Visit Ends
        {
            int processStatus = 7;
            DateTime eventTime = _repoUtil.GetNijaTime(DateTime.Now);
                        
            //get flow queue by AttID, since only an instance of Attendance can be in a flowqueue per visit
            FlowQueue eFQ = _entities.FlowQueues.FirstOrDefault(n => n.AttID == AttID);
            if (eFQ == null)
                return false;
            eFQ.CurrentStatusID = processStatus;
            eFQ.LastTimeStamp = eventTime;
            eFQ.LastUpdatedBy = userDoingTask.user_id;
            this.UpdateFlowQueue(eFQ, eventTime);

            //then, here queue detail is removed from queue table
            this.DeleteFlowQueue(AttID);

            //then generate a new process flow
            ProcessFlow newPF = new ProcessFlow();
            newPF.AttID = AttID;
            newPF.Comment = "Patient's visit ends. End report created by " + userDoingTask.Email;
            newPF.StatusID = processStatus;
            newPF.StartTime = eventTime;
            newPF.UserID = userDoingTask.user_id;
            _entities.ProcessFlows.Add(newPF);

            var result = _entities.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }

        #endregion


        #region Department/Stage Tasks that operates the Queues

        public bool StartPatientVisit(dto_Patients patient, AspNetUser userDoingTask)
        {
            if (patient == null)
                return false;

            //before we begin ensure that duplicated entry for a patient at OPD
            //will not be possible
            var isPatientInFlowQueueAtOPD = _entities.vwFlowQueueAttendances
                .FirstOrDefault(n => n.AttdPatientID == patient.ID && n.FQCurrentSTatus == 1);
            if (isPatientInFlowQueueAtOPD != null)
                return false;

            // a patient's attendance record is created
            int batchno = 0;
            TableSerialNo promoBatch = new TableSerialNo();
            string thisBatch = promoBatch.GetNextNumberString("ATTENDANCE", "ATTENDANCE",
                          "PRB", ref batchno);
            DateTime eventDate = _repoUtil.GetNijaTime(DateTime.Now);
            attendance att = new attendance();
            att.InsertID = thisBatch;
            att.PatientID = patient.ID;
            att.CardNumber = patient.CardNumber;
            att.AttDate = eventDate;
            _entities.attendances.Add(att);
            var saveResult = _entities.SaveChanges();

            //get the attendance just created
            attendance attd = _entities.attendances.FirstOrDefault(n => n.InsertID == thisBatch);
            if (attd == null)
                return false;
            
            //begin process by queuing patient at OPD
            var result = QueuePatientAtOPD(attd.ID, userDoingTask);
            
            if (saveResult > 0 && result == true)
                return true;
            else
                return false;
        }

        #endregion

        #region QUEUE GET LISTS

        public IQueryable<vwFlowQueueAttendance> AllFlowQueues()
        {
            var model = _entities.vwFlowQueueAttendances;
            return model;
        }

        public IQueryable<vwFlowQueueAttendance> GetOPDQueues()
        {
            var model = _entities.vwFlowQueueAttendances.Where(n => n.FQCurrentSTatus == 1);
            return model;
        }

        public IQueryable<vwFlowQueueAttendance> GetConsultingQueues()
        {
            var model = _entities.vwFlowQueueAttendances.Where(n => n.FQCurrentSTatus == 2);
            return model;
        }

        public IQueryable<vwFlowQueueAttendance> GetConsultingQueuesByRoomID(int conRmID)
        {
            var model = _entities.vwFlowQueueAttendances
                .Where(n => n.FQCurrentSTatus == 2 && n.AttConsultingRoom == conRmID);
            return model;
        }

        public IQueryable<vwFlowQueueAttendance> GetLabQueues()
        {
            var model = _entities.vwFlowQueueAttendances.Where(n => n.FQCurrentSTatus == 3);
            return model;
        }

        public IQueryable<vwFlowQueueAttendance> GetAdmissionQueues()
        {
            var model = _entities.vwFlowQueueAttendances.Where(n => n.FQCurrentSTatus == 4);
            return model;
        }

        public IQueryable<vwFlowQueueAttendance> GetPharmacyQueues()
        {
            var model = _entities.vwFlowQueueAttendances.Where(n => n.FQCurrentSTatus == 5);
            return model;
        }

        public IQueryable<vwFlowQueueAttendance> GetAccountsQueues()
        {
            var model = _entities.vwFlowQueueAttendances.Where(n => n.FQCurrentSTatus == 6);
            return model;
        }
        #endregion
    }
}
