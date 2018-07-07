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
            ProcessFlow lpf = _entities.ProcessFlows.LastOrDefault(n => n.AttID == fq.AttID);
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
            _entities.FlowQueues.Add(newFQ);

            var result = _entities.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }

        public bool QueuePatientAtConsulting(int AttID, AspNetUser userDoingTask) //Process Status 2 - At Consulting
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
            this.UpdateFlowQueue(eFQ, eventTime);

            //then generate a new process flow
            ProcessFlow newPF = new ProcessFlow();
            newPF.AttID = AttID;
            newPF.Comment = "Patient sent to Consulting by " + userDoingTask.Email;
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
    }
}
