//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Repo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class labrequest
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
    
        public virtual attendance attendance { get; set; }
    }
}
