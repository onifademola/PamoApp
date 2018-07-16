using Microsoft.AspNet.Identity;
using Repo.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Hubs;

namespace Web.Controllers
{
    public class MainController : Controller
    {
        public IRepo_Util _irepoUtil;
        public IRepo_PFlow _irepoPflow;
        public IRepo_Patient _irepoPatient;

        public MainController(IRepo_Util irepoUtil, IRepo_PFlow irepoPflow, IRepo_Patient irepoPatient)
        {
            _irepoUtil = irepoUtil;
            _irepoPflow = irepoPflow;
            _irepoPatient = irepoPatient;
        }

        // GET: Main
        public ActionResult Home()
        {
            return View();
        }

        #region FRONT DESK CODECS
        public ActionResult FrontDesk()
        {
            ViewData["vstatus"] = _irepoUtil.ProcessStatuses();
            return View();
        }

        //[HttpPost]
        //public ActionResult SendToOPD(int[] selectedPtIds)
        //{
        //    try
        //    {
        //        foreach (var stud in selectedPtIds) //selectedIDsHF2.Split(new Char[] { ',' }))
        //        {

        //            int patientID = Convert.ToInt32(stud);
        //            if (patientID < 0)
        //                return null;
        //            string uid = User.Identity.GetUserId();
        //            var userDoingTask = _irepoPflow.GetUserDoingTask(uid);
        //            var patient = _irepoPatient.GetPatientById(patientID);
        //            if (patient != null && userDoingTask != null)
        //            {
        //                _irepoPflow.StartPatientVisit(patient, userDoingTask);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return Redirect(System.Web.HttpContext.Current.Request.UrlReferrer.ToString());
        //}

        [HttpPost]
        public ActionResult SendToOPD(int selectedPtIds)
        {
            bool result = false;
            int patientID = Convert.ToInt32(selectedPtIds);
            if (patientID < 0)
                return null;
            string uid = User.Identity.GetUserId();
            var userDoingTask = _irepoPflow.GetUserDoingTask(uid);
            var patient = _irepoPatient.GetPatientById(patientID);
            if (patient != null && userDoingTask != null)
            {
                bool res = _irepoPflow.StartPatientVisit(patient, userDoingTask);
                result = res;
            }
                        
            if (result == true)
            {
                FlowQueueHub.NotifyOPD();
                var data = new[]
                 {
                    new
                    {
                        resp = "ok",
                        mesag = "Patient sent to OPD !"
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
            //return Redirect(System.Web.HttpContext.Current.Request.UrlReferrer.ToString());
        }
        #endregion

        #region OPD CODECS
        public ActionResult Opd()
        {
            ViewData["vstatus"] = _irepoUtil.ProcessStatuses();
            ViewData["recBy"] = _irepoUtil.UserForGrid();
            return View();
        }

        [HttpPost]
        public ActionResult SendToConsulting(int attdID, int consultnRmID)
        {
            bool result = false;
            int attdsID = Convert.ToInt32(attdID);
            int consRmID = Convert.ToInt32(consultnRmID);
            if (attdsID < 0 || consRmID < 0)
                return null;
            string uid = User.Identity.GetUserId();
            var userDoingTask = _irepoPflow.GetUserDoingTask(uid);
            if (userDoingTask != null)
            {
                bool res = _irepoPflow.QueuePatientAtConsulting(attdsID, consRmID, userDoingTask);
                result = res;
            }

            if (result == true)
            {
                FlowQueueHub.NotifyConsulting(consRmID);
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
        #endregion

        #region CONSULTING CODECS
        public JsonResult GetConsultingRoomsForGrid()
        {
            return Json( _irepoUtil.ConsultingRoomForGrid(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Consulting()
        {
            ViewData["vstatus"] = _irepoUtil.ProcessStatuses();
            ViewData["conRoom"] = _irepoUtil.ConsultingRoomForGrid();
            return View();
        }

        public ActionResult SendToPharmacy()
        {
            return null;
        }

        public ActionResult SendToLab()
        {
            return null;
        }

        public ActionResult SendToAdmission()
        {
            return null;
        }

        #endregion
    }
}