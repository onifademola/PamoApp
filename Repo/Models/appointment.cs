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
    
    public partial class appointment
    {
        public int AppointmentID { get; set; }
        public Nullable<System.DateTime> C_Date { get; set; }
        public Nullable<System.DateTime> AppointmentDate { get; set; }
        public Nullable<int> DoctorID { get; set; }
        public string Doctor { get; set; }
        public Nullable<int> PatientID { get; set; }
        public string CardNumber { get; set; }
        public Nullable<int> AttID { get; set; }
        public string ApptType { get; set; }
        public string C_Time { get; set; }
    }
}
