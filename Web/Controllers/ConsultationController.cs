using Microsoft.AspNet.Identity;
using Repo.DTOs;
using Repo.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ConsultationController : Controller
    {
        public IRepo_Util _irepoUtil;
        public IRepo_PFlow _irepoPFlow;

        public ConsultationController(IRepo_Util irepoUtil, IRepo_PFlow irepoPFlow)
        {
            _irepoUtil = irepoUtil;
            _irepoPFlow = irepoPFlow;
        }
        
        public string GetDocReportForAttendance(int attdId)
        {
            var attd = _irepoPFlow.GetAttendance(attdId);
            if(attd != null)
            {
                return attd.Note;
            }
            return "Data not found";
        }

        [HttpPost]        
        public ActionResult UpdateDocReportForAttendance(int attdID, string note)
        {
            bool result = false;
            int attdsID = Convert.ToInt32(attdID);
            if (attdsID < 0)
                return null;
            string uid = User.Identity.GetUserId();
            var userDoingTask = _irepoPFlow.GetUserDoingTask(uid);
            if (userDoingTask == null)
            {
                var data = new[]
                 {
                    new
                    {
                        resp = "error",
                        mesag = "Process aborted, we cannot identify a valid user for this task. Please confirm that you are logged in."
                    }
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }            
            if (note != null)
            {
                dto_Attendance dtoAtt = new dto_Attendance();
                dtoAtt.ID = attdsID;
                dtoAtt.Note = note;
                bool res = _irepoPFlow.UpdateAttendance(dtoAtt);
                result = res;
            }

            if (result == true)
            {
                var data = new[]
                 {
                    new
                    {
                        resp = "ok",
                        mesag = "Patient sent to Consulting !"
                    }
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = new[]
                 {
                    new
                    {
                        resp = "error",
                        mesag = "Process aborted, something went wrong. Please check your task and retry, or contact Support."
                    }
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
    }
}