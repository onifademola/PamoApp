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
    
    public partial class cash
    {
        public long ID { get; set; }
        public string Type { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Classification { get; set; }
        public string Particulars { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Source { get; set; }
        public string Recipient { get; set; }
        public string Paid { get; set; }
        public string Remark { get; set; }
        public string Bank { get; set; }
        public string Account { get; set; }
    }
}
