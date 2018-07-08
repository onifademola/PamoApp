using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Repo.DTOs;
using Repo.Repos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Web.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        public IRepo_Patient _patientRepo;

        public PatientController(IRepo_Patient patientRepo)
        {
            _patientRepo = patientRepo;
        }

        // GET: Patient
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult PatientsJson_Read()
        {
            var model = _patientRepo.GetAllPatients();
            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
            //return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Patients_Read([DataSourceRequest] DataSourceRequest request)
        {
            var model = _patientRepo.GetAllPatients();
            JsonResult result = Json(model.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Patients_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<dto_Patients> patients)
        {
            foreach (var patient in patients)
            {
                if (patient != null && ModelState.IsValid)
                {
                    _patientRepo.AddPatient(patient);
                }
            }

            return Json(patients.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Patients_BatchUpdate([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<dto_Patients> patients)
        {
            foreach (var patient in patients)
            {
                if (patient != null)
                {
                    _patientRepo.EditPatient(patient);
                }
            }

            return Json(patients.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Patients_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<dto_Patients> patients)
        {
            foreach (var patient in patients)
            {
                if (patient != null && ModelState.IsValid)
                {
                    _patientRepo.DeletePatient(patient.ID);
                }
            }

            return Json(patients.ToDataSourceResult(request, ModelState));
        }

    }
}