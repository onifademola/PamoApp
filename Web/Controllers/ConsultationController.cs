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
        public IRepo_Consulting _irepoConsult;

        public ConsultationController(IRepo_Util irepoUtil, IRepo_PFlow irepoPFlow, IRepo_Consulting irepoConsult)
        {
            _irepoUtil = irepoUtil;
            _irepoPFlow = irepoPFlow;
            _irepoConsult = irepoConsult;
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

        //[HttpPost]
        [ValidateInput(false)]
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
                dtoAtt.DoctorID = userDoingTask.user_id;
                dtoAtt.Consultant = userDoingTask.Email;
                bool res = _irepoConsult.UpdateDocRecAttendance(dtoAtt);
                result = res;
            }

            if (result == true)
            {
                var data = new[]
                 {
                    new
                    {
                        resp = "ok",
                        mesag = "Report auto updated successfully!"
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
                        mesag = "Process aborted, something went wrong. Report not updated."
                    }
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
    }
}