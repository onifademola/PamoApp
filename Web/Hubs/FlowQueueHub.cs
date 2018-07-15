using System;
using System.Collections.Generic;
using System.Linq;
using Repo.Models;
using Microsoft.AspNet.SignalR;
using Repo.Repos;
using System.Web.Mvc;
using System.Configuration;
using Microsoft.AspNet.SignalR.Hubs;

namespace Web.Hubs
{
    public class FlowQueueHub : Hub
    {
        //private static string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        Repo_PFlow _irepoPFlow = new Repo_PFlow();
                
        public void Hello()
        {
            Clients.All.hello();
        }

        [HubMethodName("sendMessages")]
        public static void SendMessages()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<FlowQueueHub>();
            context.Clients.All.updateMessages();
        }

        public static void NotifyOPD()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<FlowQueueHub>();
            context.Clients.All.updateMessages();
        }

        public static void NotifyConsulting()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<FlowQueueHub>();
            context.Clients.All.updateConsultingMsgs();
        }

        public IQueryable<vwFlowQueueAttendance> GetAllFlowQueues()
        {
            var mode = _irepoPFlow.AllFlowQueues();
            var model = mode;
            return model;
        }

        public IQueryable<vwFlowQueueAttendance> GetOPDFlowQueues()
        {
            var model = _irepoPFlow.GetOPDQueues();
            return model;
        }

        public IQueryable<vwFlowQueueAttendance> GetConsultingFlowQueues()
        {
            var model = _irepoPFlow.GetConsultingQueues();
            return model;
        }

        public IQueryable<vwFlowQueueAttendance> GetLabFlowQueues()
        {
            var model = _irepoPFlow.GetLabQueues();
            return model;
        }

        public IQueryable<vwFlowQueueAttendance> GetAdmissionFlowQueues()
        {
            var model = _irepoPFlow.GetAdmissionQueues();
            return model;
        }

        public IQueryable<vwFlowQueueAttendance> GetPharmacyFlowQueues()
        {
            var model = _irepoPFlow.GetPharmacyQueues();
            return model;
        }

        public IQueryable<vwFlowQueueAttendance> GetAccountsFlowQueues()
        {
            var model = _irepoPFlow.GetAccountsQueues();
            return model;
        }

    }
}