using System;

namespace Repo.DTOs
{
    public class dto_Prescription
    {
        public int ID { get; set; }
        public Nullable<int> AttID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string CardNumber { get; set; }
        public string Prescription1 { get; set; }
        public Nullable<long> Qty { get; set; }
        public string Dose { get; set; }
        public Nullable<long> Duration { get; set; }
        public string Treated { get; set; }
        public Nullable<short> Referred { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string Mgt { get; set; }
        public string SCode { get; set; }
        public Nullable<short> Paid { get; set; }
        public string Staff { get; set; }
        public string Comment { get; set; }
        public Nullable<int> DispensedByID { get; set; }
        public string DispensedBy { get; set; }
        public Nullable<System.DateTime> DispensedOn { get; set; }
    }
}
