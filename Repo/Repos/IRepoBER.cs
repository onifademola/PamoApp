using Repo.Models;

namespace Repo.Repos
{
    public interface IRepoBER : IRepositoryBase<User>
    {
        bool GenerateUserUniqueID();
        bool FixPatientAttendanceTB();
        bool FixDoctorAttendanceTB();
        bool FixDoctorProcedurTB();
        bool FixPatientProcedurTB();
        bool FixPatientPhyExamTB();
        bool FixPatientRoundsTB();
        bool FixDoctorPhyExamTB();
        bool FixDoctorRoundsTB();
        bool FixPatientPComplainTB();
        bool FixPatientBiovitalsTB();
        bool FixPatientDeliveryTB();
        bool FixPatientDiagnosisTB();
        bool FixPatientInvestigationTB();
        bool FixPatientNursenoteTB();
        bool FixPatientScanReportTB();
        bool FixPatientSurgeryTB();
        bool FixPatientTblRecTB();
        bool FixPatientWardTB();
        bool FixPatientAppointmentTB();
        bool FixDoctorAppointmentTB();
        bool FixNurseNursenoteTB();
        bool FixComplainUserTB();
        bool FixAttendanceConsultingRoomTB();
        bool FixDiagnosisDiagnosedBy();
    }
}
