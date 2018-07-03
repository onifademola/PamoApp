using Repo.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class BackEndRepairController : Controller
    {
        // GET: BackEndRepair
        public readonly IRepoBER _ber;

        public BackEndRepairController(IRepoBER ber)
        {
            _ber = ber;
        }

        public ActionResult Index()
        {
            return View();
        }

        public bool GenerateUserUniqueId()
        {
            bool fix = _ber.GenerateUserUniqueID();
            return fix;
        }

        public bool FixDoctorProcedurTB()
        {
            bool fix = _ber.FixDoctorProcedurTB();
            return fix;
        }

        public bool FixPatientAttendanceTB()
        {
            bool fix = _ber.FixPatientAttendanceTB();
            return fix;
        }

        public bool FixPatientPhyExamTB()
        {
            bool fix = _ber.FixPatientPhyExamTB();
            return fix;
        }

        public bool FixPatientProcedurTB()
        {
            bool fix = _ber.FixPatientProcedurTB();
            return fix;
        }

        //public bool FixPatientRoundsTB()
        //{
        //    bool fix = _ber.FixPatientRoundsTB();
        //    return fix;
        //}

        public bool FixDoctorPhyExamTB()
        {
            bool fix = _ber.FixDoctorPhyExamTB();
            return fix;
        }

        //public bool FixDoctorRoundsTB()
        //{
        //    bool fix = _ber.FixDoctorRoundsTB();
        //    return fix;
        //}

        public bool FixPatientPComplainTB()
        {
            bool fix = _ber.FixPatientPComplainTB();
            return fix;
        }

        public bool FixPatientBiovitalsTB()
        {
            bool fix = _ber.FixPatientBiovitalsTB();
            return fix;
        }

        public bool FixPatientDeliveryTB()
        {
            bool fix = _ber.FixPatientDeliveryTB();
            return fix;
        }

        public bool FixPatientDiagnosisTB()
        {
            bool fix = _ber.FixPatientDiagnosisTB();
            return fix;
        }

        public bool FixPatientInvestigationTB()
        {
            bool fix = _ber.FixPatientInvestigationTB();
            return fix;
        }

        public bool FixPatientNursenoteTB()
        {
            bool fix = _ber.FixPatientNursenoteTB();
            return fix;
        }

        public bool FixPatientScanReportTB()
        {
            bool fix = _ber.FixPatientScanReportTB();
            return fix;
        }

        public bool FixPatientSurgeryTB()
        {
            bool fix = _ber.FixPatientSurgeryTB();
            return fix;
        }

        public bool FixPatientTblRecTB()
        {
            bool fix = _ber.FixPatientTblRecTB();
            return fix;
        }

        public bool FixPatientWardTB()
        {
            bool fix = _ber.FixPatientWardTB();
            return fix;
        }

        public bool FixPatientAppointmentTB()
        {
            bool fix = _ber.FixPatientAppointmentTB();
            return fix;
        }

        public bool FixDoctorAppointmentTB()
        {
            bool fix = _ber.FixDoctorAppointmentTB();
            return fix;
        }

        public bool FixNurseNursenoteTB()
        {
            bool fix = _ber.FixNurseNursenoteTB();
            return fix;
        }

        public bool FixComplainUserTB()
        {
            bool fix = _ber.FixComplainUserTB();
            return fix;
        }

        public bool FixAttendanceConsultingRoomTB()
        {
            bool fix = _ber.FixAttendanceConsultingRoomTB();
            return fix;
        }

        public bool FixDiagnosisDiagnosedBy()
        {
            bool fix = _ber.FixDiagnosisDiagnosedBy();
            return fix;
        }

        public bool FixDoctorAttendanceTB()
        {
            bool fix = _ber.UpdateUserAcc();//_ber.FixDoctorAttendanceTB();
            return fix;
        }
    }
}