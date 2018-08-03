using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.DTOs
{
    public class dto_WardRound
    {
        public int ID { get; set; }
        public Nullable<int> AdmissionID { get; set; }
        public Nullable<int> PatientID { get; set; }
        public string CardNumber { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Hour { get; set; }
        public string Note { get; set; }
        public string Doctor { get; set; }
        public Nullable<int> DoctorID { get; set; }
    }
}
