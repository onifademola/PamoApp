using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.DTOs
{
    public class dto_Patients
    {        
        public int ID { get; set; }
        public string CardNumber { get; set; }
        public string HMO { get; set; }
        public string Code { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Location { get; set; }
        public string Company { get; set; }
        public string CompanyCode { get; set; }
        public string Status { get; set; }
        public string Position { get; set; }
        public string Designation { get; set; }
        public string Occupation { get; set; }
        public Nullable<System.DateTime> DoB { get; set; }
        public string Dept { get; set; }
        public string Sex { get; set; }
        public string MaritalStatus { get; set; }
        public Nullable<long> Age { get; set; }
        public string BloodGroup { get; set; }
        public string NationalID { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public Nullable<short> Diabetes { get; set; }
        public Nullable<short> Epilepsy { get; set; }
        public Nullable<short> Hypertension { get; set; }
        public Nullable<short> SickleCell { get; set; }
        public Nullable<short> Allergies { get; set; }
        public string Others { get; set; }
        public Nullable<System.DateTime> RegDate { get; set; }
        public string NHISNo { get; set; }
        public string NKin { get; set; }
        public string NKContact { get; set; }
        public string Scheme { get; set; }
        public string SchemeID { get; set; }
        public string Religion { get; set; }
        public string PostalAddress { get; set; }
        public string Email { get; set; }
        public string District { get; set; }
        public string SubDistrict { get; set; }
        public string AgeGroup { get; set; }
        public string NHIS { get; set; }
    }
}
