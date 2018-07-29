using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.DTOs
{
    public class dto_Attendance
    {
        public int ID { get; set; }
        public string InsertID { get; set; }
        public Nullable<int> PatientID { get; set; }
        public string CardNumber { get; set; }
        public Nullable<System.DateTime> AttDate { get; set; }
        public Nullable<bool> Attended { get; set; }
        public Nullable<int> DoctorID { get; set; }
        public string Consultant { get; set; }
        public Nullable<int> ConsultingRoomID { get; set; }
        public string Room { get; set; }
        public string ServiceType { get; set; }
        public string Note { get; set; }
    }
}
