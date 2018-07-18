using Repo.DTOs;
using Repo.Models;
using System;
using System.Linq;

namespace Repo.Repos
{
    public interface IRepo_PFlow : IRepositoryBase<ProcessFlow>
    {
        AspNetUser GetUserDoingTask(string userASPIdentityID);
        
        bool QueuePatientAtOPDFromLab(int AttID, AspNetUser userDoingTask);
        bool QueuePatientAtConsulting(int AttID, int consultingRoomID, AspNetUser userDoingTask);
        bool QueuePatientAtLab(int AttID, AspNetUser userDoingTask);
        bool QueuePatientAtAdmission(int AttID, AspNetUser userDoingTask);
        bool QueuePatientAtPharmacy(int AttID, AspNetUser userDoingTask);
        bool QueuePatientWithAccounts(int AttID, AspNetUser userDoingTask);
        bool QueueEnds(int AttID, AspNetUser userDoingTask);

        #region Department/Stage Tasks that operates the Queues
        bool StartPatientVisit(dto_Patients patient, AspNetUser userDoingTask);
        #endregion


        #region QUEUE GET LISTS
        IQueryable<vwFlowQueueAttendance> AllFlowQueues();
        IQueryable<vwFlowQueueAttendance> GetOPDQueues();
        IQueryable<vwFlowQueueAttendance> GetConsultingQueues();
        IQueryable<vwFlowQueueAttendance> GetConsultingQueuesByRoomID(int conRmID);
        IQueryable<vwFlowQueueAttendance> GetLabQueues();
        IQueryable<vwFlowQueueAttendance> GetAdmissionQueues();
        IQueryable<vwFlowQueueAttendance> GetPharmacyQueues();
        IQueryable<vwFlowQueueAttendance> GetAccountsQueues();
        #endregion
    }
}
