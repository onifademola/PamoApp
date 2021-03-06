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
    
    public partial class stock
    {
        public int ID { get; set; }
        public string StockCode { get; set; }
        public string Category { get; set; }
        public string BrandName { get; set; }
        public string StockName { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string Supplier { get; set; }
        public Nullable<decimal> UnitCost { get; set; }
        public System.DateTime ManufacturedDate { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public Nullable<int> ReorderLevel { get; set; }
        public Nullable<int> UnitsInStock { get; set; }
        public string Location { get; set; }
        public Nullable<decimal> SellingPrice { get; set; }
        public string Type { get; set; }
        public Nullable<decimal> NHISPrice { get; set; }
        public string Unit { get; set; }
    }
}
