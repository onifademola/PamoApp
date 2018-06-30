﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PamoDbEntities : DbContext
    {
        public PamoDbEntities()
            : base("name=PamoDbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<anc> ancs { get; set; }
        public virtual DbSet<appointment> appointments { get; set; }
        public virtual DbSet<attendance> attendances { get; set; }
        public virtual DbSet<bank> banks { get; set; }
        public virtual DbSet<bill> bills { get; set; }
        public virtual DbSet<billType> billTypes { get; set; }
        public virtual DbSet<billupdate> billupdates { get; set; }
        public virtual DbSet<biovital> biovitals { get; set; }
        public virtual DbSet<cash> cashes { get; set; }
        public virtual DbSet<company> companies { get; set; }
        public virtual DbSet<consultingRoom> consultingRooms { get; set; }
        public virtual DbSet<contract> contracts { get; set; }
        public virtual DbSet<delivery> deliveries { get; set; }
        public virtual DbSet<department> departments { get; set; }
        public virtual DbSet<diagnosi> diagnosis { get; set; }
        public virtual DbSet<diagnosis_type> diagnosis_types { get; set; }
        public virtual DbSet<doctor> doctors { get; set; }
        public virtual DbSet<drround> drrounds { get; set; }
        public virtual DbSet<drug> drugs { get; set; }
        public virtual DbSet<@event> events { get; set; }
        public virtual DbSet<event1> events1 { get; set; }
        public virtual DbSet<grouping> groupings { get; set; }
        public virtual DbSet<haematology> haematologies { get; set; }
        public virtual DbSet<head> heads { get; set; }
        public virtual DbSet<history> histories { get; set; }
        public virtual DbSet<hmo> hmoes { get; set; }
        public virtual DbSet<investigation> investigations { get; set; }
        public virtual DbSet<investigatn> investigatns { get; set; }
        public virtual DbSet<lab> labs { get; set; }
        public virtual DbSet<labh> labhs { get; set; }
        public virtual DbSet<labp> labps { get; set; }
        public virtual DbSet<labrequest> labrequests { get; set; }
        public virtual DbSet<labw> labws { get; set; }
        public virtual DbSet<lga> lgas { get; set; }
        public virtual DbSet<NurseNote> NurseNotes { get; set; }
        public virtual DbSet<patient> patients { get; set; }
        public virtual DbSet<PatientComplaint> PatientComplaints { get; set; }
        public virtual DbSet<phyexam> phyexams { get; set; }
        public virtual DbSet<procedur> procedurs { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<round> rounds { get; set; }
        public virtual DbSet<scanreport> scanreports { get; set; }
        public virtual DbSet<stock> stocks { get; set; }
        public virtual DbSet<stockCategory> stockCategories { get; set; }
        public virtual DbSet<surgery> surgeries { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tarrif> tarrifs { get; set; }
        public virtual DbSet<tbl_complaint> tbl_complaint { get; set; }
        public virtual DbSet<tbl_complaint_category> tbl_complaint_category { get; set; }
        public virtual DbSet<tbl_rec> tbl_rec { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<UserRegistrationToken> UserRegistrationTokens { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<ward> wards { get; set; }
        public virtual DbSet<WardType> WardTypes { get; set; }
        public virtual DbSet<grade_level> grade_levels { get; set; }
        public virtual DbSet<location> locations { get; set; }
        public virtual DbSet<monitor> monitors { get; set; }
        public virtual DbSet<mstock> mstocks { get; set; }
        public virtual DbSet<nhis_mstock> nhis_mstocks { get; set; }
        public virtual DbSet<nhis_stock> nhis_stocks { get; set; }
        public virtual DbSet<office> offices { get; set; }
        public virtual DbSet<pnc> pncs { get; set; }
        public virtual DbSet<prescription> prescriptions { get; set; }
        public virtual DbSet<procedure> procedures { get; set; }
        public virtual DbSet<profession> professions { get; set; }
        public virtual DbSet<requisition> requisitions { get; set; }
        public virtual DbSet<restock> restocks { get; set; }
        public virtual DbSet<sale> sales { get; set; }
        public virtual DbSet<scan> scans { get; set; }
        public virtual DbSet<service> services { get; set; }
        public virtual DbSet<staff> staffs { get; set; }
        public virtual DbSet<state> states { get; set; }
        public virtual DbSet<status> status { get; set; }
        public virtual DbSet<unit> units { get; set; }
    }
}
