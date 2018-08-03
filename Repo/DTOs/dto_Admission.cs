using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.DTOs
{
    public class dto_Admission
    {
        public int ID { get; set; }
        public Nullable<int> AttID { get; set; }
        public Nullable<int> ClinicWardID { get; set; }
        public string WardName { get; set; }
        public Nullable<System.DateTime> C_Date { get; set; }
        public string ActivityType { get; set; }
        public string CardNumber { get; set; }
        public Nullable<int> PatientID { get; set; }
        public string PatientName { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public string Official { get; set; }
        public string Designation { get; set; }
        public string Dept { get; set; }
        public string Hall { get; set; }
        public string Remark { get; set; }
        public string PostedBy { get; set; }
        public Nullable<int> PostedByID { get; set; }
        public Nullable<int> StatusID { get; set; }
    }
}
