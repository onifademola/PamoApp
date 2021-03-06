﻿using Microsoft.AspNet.Identity;
using Repo.DTOs;
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
        public IRepo_Consulting _irepoConsult;

        public MainController(IRepo_Util irepoUtil, IRepo_PFlow irepoPflow, IRepo_Patient irepoPatient, IRepo_Consulting irepoConsult)
        {
            _irepoUtil = irepoUtil;
            _irepoPflow = irepoPflow;
            _irepoPatient = irepoPatient;
            _irepoConsult = irepoConsult;
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
                FlowQueueHub.NotifyFrontDesk("Fresh update !");
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
                FlowQueueHub.NotifyFrontDesk("Fresh update !");
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

        public ActionResult SendToPharmacy(int attdID)
        {
            bool result = false;
            int attdsID = Convert.ToInt32(attdID);
            if (attdsID < 0)
                return null;
            string uid = User.Identity.GetUserId();
            var userDoingTask = _irepoPflow.GetUserDoingTask(uid);
            if (userDoingTask != null)
            {
                bool res = _irepoPflow.QueuePatientAtPharmacy(attdsID, userDoingTask);
                result = res;
            }

            if (result == true)
            {
                FlowQueueHub.NotifyPharmacy();
                FlowQueueHub.NotifyFrontDesk("Fresh update !");
                var data = new[]
                 {
                    new
                    {
                        resp = "ok",
                        mesag = "Patient sent to Pharmacy !"
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

        public ActionResult SendToLab(int attdID)
        {
            bool result = false;
            int attdsID = Convert.ToInt32(attdID);
            if (attdsID < 0)
                return null;
            string uid = User.Identity.GetUserId();
            var userDoingTask = _irepoPflow.GetUserDoingTask(uid);
            if (userDoingTask != null)
            {
                bool res = _irepoPflow.QueuePatientAtLab(attdsID, userDoingTask);
                result = res;
            }

            if (result == true)
            {
                FlowQueueHub.NotifyLab();
                FlowQueueHub.NotifyFrontDesk("Fresh update !");
                var data = new[]
                 {
                    new
                    {
                        resp = "ok",
                        mesag = "Patient sent to the Lab !"
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

        public ActionResult SendToAdmission(int attdID)
        {
            bool result = false;
            int attdsID = Convert.ToInt32(attdID);
            if (attdsID < 0)
                return null;
            string uid = User.Identity.GetUserId();
            var userDoingTask = _irepoPflow.GetUserDoingTask(uid);
            if (userDoingTask != null)
            {
                bool res = _irepoPflow.QueuePatientAtAdmission(attdsID, userDoingTask);
                result = res;
            }

            if (result == true)
            {
                FlowQueueHub.NotifyAdmission();
                FlowQueueHub.NotifyFrontDesk("Fresh update !");
                var data = new[]
                 {
                    new
                    {
                        resp = "ok",
                        mesag = "Patient has been admitted !"
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

        public ActionResult AdmitPatient(int attdID, int wardID, string rmk)
        {
            bool result = false;
            DateTime eventTime = _irepoUtil.GetNijaTime(DateTime.Now);
            if (attdID < 0)
                return null;
            string uid = User.Identity.GetUserId();
            var userDoingTask = _irepoPflow.GetUserDoingTask(uid);
            
            if (userDoingTask == null)
            {
                var data = new[]
                 {
                    new
                    {
                        resp = "error",
                        mesag = "Process aborted, We cannot find your login details, Please confirm that you are logged in."
                    }
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            RepositoryActionResult<dto_Admission> res;
            dto_Admission dtoAdm = new dto_Admission();
            dtoAdm.ClinicWardID = wardID;
            dtoAdm.Remark = rmk;
            dtoAdm.StatusID = 1;
            dtoAdm.C_Date = eventTime;
            res = _irepoConsult.AddAdmission(dtoAdm, attdID);
            //result = true;
            
            if (res.Status == RepositoryActionStatus.Created)
            {
                var data = new[]
                 {
                    new
                    {
                        resp = "ok",
                        mesag = "Patient has been admitted !"
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

        #region LAB CODECS
        public ActionResult Lab()
        {
            ViewData["vstatus"] = _irepoUtil.ProcessStatuses();
            return View();
        }

        public ActionResult ReturnToOpdFromLab(int attdID)
        {
            bool result = false;
            int attdsID = Convert.ToInt32(attdID);
            if (attdsID < 0)
                return null;
            string uid = User.Identity.GetUserId();
            var userDoingTask = _irepoPflow.GetUserDoingTask(uid);
            if (userDoingTask != null)
            {
                bool res = _irepoPflow.QueuePatientAtOPDFromLab(attdsID, userDoingTask);
                result = res;
            }

            if (result == true)
            {
                FlowQueueHub.NotifyOPD();
                FlowQueueHub.NotifyFrontDesk("Fresh update !");
                var data = new[]
                 {
                    new
                    {
                        resp = "ok",
                        mesag = "Patient returned to OPD !"
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

        #region PHARMACY CODECS
        public ActionResult Pharmacy()
        {
            ViewData["vstatus"] = _irepoUtil.ProcessStatuses();
            return View();
        }

        public ActionResult SendToAccounts(int attdID)
        {
            bool result = false;
            int attdsID = Convert.ToInt32(attdID);
            if (attdsID < 0)
                return null;
            string uid = User.Identity.GetUserId();
            var userDoingTask = _irepoPflow.GetUserDoingTask(uid);
            if (userDoingTask != null)
            {
                bool res = _irepoPflow.QueuePatientWithAccounts(attdsID, userDoingTask);
                result = res;
            }

            if (result == true)
            {
                FlowQueueHub.NotifyAccount();
                FlowQueueHub.NotifyFrontDesk("Fresh update !");
                var data = new[]
                 {
                    new
                    {
                        resp = "ok",
                        mesag = "Patient sent to Accounts for payment !"
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

        #region ACCOUNTS CODECS
        public ActionResult Accounts()
        {
            ViewData["vstatus"] = _irepoUtil.ProcessStatuses();
            return View();
        }

        public ActionResult EndPatientsVisit(int attdID)
        {
            bool result = false;
            int attdsID = Convert.ToInt32(attdID);
            if (attdsID < 0)
                return null;
            string uid = User.Identity.GetUserId();
            var userDoingTask = _irepoPflow.GetUserDoingTask(uid);
            if (userDoingTask != null)
            {
                bool res = _irepoPflow.QueueEnds(attdsID, userDoingTask);
                result = res;
            }

            if (result == true)
            {
                FlowQueueHub.NotifyFrontDesk("A patient's visit has ended !");
                var data = new[]
                 {
                    new
                    {
                        resp = "ok",
                        mesag = "Patient's visit has ended !"
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

        #region ADMISSION CODECS
        public JsonResult GetClinicWardsForGrid()
        {
            return Json(_irepoUtil.ClinicWardForGrid(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Admission()
        {
            ViewData["vstatus"] = _irepoUtil.ProcessStatuses();
            ViewData["admstatus"] = _irepoUtil.AdmissionStatus();
            ViewData["wrdstatus"] = _irepoUtil.ClinicWardForMainGrid();
            return View();
        }
        
        public ActionResult DischargeAndSendToPharmacy(int attdID)
        {
            bool result = false;
            int attdsID = Convert.ToInt32(attdID);
            if (attdsID < 0)
                return null;
            string uid = User.Identity.GetUserId();
            var userDoingTask = _irepoPflow.GetUserDoingTask(uid);
            if (userDoingTask != null)
            {
                //----STILL NEED TO WRITE CODE TO DISCHARGE PATIENT HERE----//
                bool res = _irepoPflow.QueuePatientAtPharmacy(attdsID, userDoingTask);
                result = res;
            }

            if (result == true)
            {
                FlowQueueHub.NotifyPharmacy();
                FlowQueueHub.NotifyFrontDesk("Fresh update !");
                var data = new[]
                 {
                    new
                    {
                        resp = "ok",
                        mesag = "Patient discharged and sent to Pharmacy !"
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

        #region WARD ROUND CODECS
        public ActionResult WardRound()
        {
            ViewData["admstatus"] = _irepoUtil.AdmissionStatus();
            return View();
        }
        #endregion
    }
}