using System;

namespace Repo.DTOs
{
    public class dto_LabRequest
    {
        public int ID { get; set; }
        public Nullable<int> PatientID { get; set; }
        public Nullable<int> AttID { get; set; }
        public string CardNumber { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Test { get; set; }
        public string Remark { get; set; }
        public Nullable<int> DoctorID { get; set; }
        public string Doctor { get; set; }
        public string Status { get; set; }
        public Nullable<short> Paid { get; set; }
        public string DoneBy { get; set; }
        public Nullable<int> DoneByID { get; set; }
        public Nullable<System.DateTime> DoneOn { get; set; }
    }
}
