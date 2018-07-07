using Repo.Models;
using System;

namespace Repo.Repos
{
    public interface IRepo_PFlow : IRepositoryBase<ProcessFlow>
    {
        //bool AddNewFlowQueue(FlowQueue fq);
        //bool UpdateFlowQueue(FlowQueue fq, DateTime eventTime);
        //bool DeleteFlowQueue(int id);

        bool QueuePatientAtOPD(int AttID, AspNetUser userDoingTask);
        bool QueuePatientAtConsulting(int AttID, AspNetUser userDoingTask);
        bool QueuePatientAtLab(int AttID, AspNetUser userDoingTask);
        bool QueuePatientAtAdmission(int AttID, AspNetUser userDoingTask);
        bool QueuePatientAtPharmacy(int AttID, AspNetUser userDoingTask);
        bool QueuePatientWithAccounts(int AttID, AspNetUser userDoingTask);
        bool QueueEnds(int AttID, AspNetUser userDoingTask);
    }
}
