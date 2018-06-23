using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.DTOs
{
    public class dto_Patients
    {
        public long ID { get; set; }
        public string Card_Number { get; set; }
        public string HMO { get; set; }
        public string Code { get; set; }
        public string Family_Name { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public string Occupation { get; set; }
        public Nullable<System.DateTime> DoB { get; set; }
        public string Sex { get; set; }
        public string Marital_Status { get; set; }
        public Nullable<long> Age { get; set; }
        public string Blood_Group { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public Nullable<short> Diabetes { get; set; }
        public Nullable<short> Epilepsy { get; set; }
        public Nullable<short> Hypertension { get; set; }
        public Nullable<short> Sickle_Cell { get; set; }
        public Nullable<short> Allergies { get; set; }
        public Nullable<System.DateTime> Reg_Date { get; set; }
        public string NHIS_No { get; set; }
        public string NKin { get; set; }
        public string NKContact { get; set; }
        public string SchemeID { get; set; }
        public string Religion { get; set; }
        public string Postal_Address { get; set; }
        public string email { get; set; }
    }
}
