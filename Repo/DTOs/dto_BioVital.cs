using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.DTOs
{
    public class dto_BioVital
    {
        public int ID { get; set; }
        public Nullable<int> PatientID { get; set; }
        public string CardNumber { get; set; }
        public Nullable<System.DateTime> C_Date { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public Nullable<decimal> Height { get; set; }
        public string BP { get; set; }
        public string Fasting { get; set; }
        public string Mass { get; set; }
        public Nullable<int> RecordedById { get; set; }
        public string RecordedBy { get; set; }
        public string Pulse { get; set; }
        public string Temp { get; set; }
        public string RBS { get; set; }
    }
}
