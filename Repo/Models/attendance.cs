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
    
    public partial class attendance
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public attendance()
        {
            this.Admissions = new HashSet<Admission>();
            this.prescriptions = new HashSet<prescription>();
            this.ProcessFlows = new HashSet<ProcessFlow>();
        }
    
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
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Admission> Admissions { get; set; }
        public virtual patient patient { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<prescription> prescriptions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProcessFlow> ProcessFlows { get; set; }
    }
}
