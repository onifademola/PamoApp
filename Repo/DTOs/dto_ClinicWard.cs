using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.DTOs
{
    public class dto_ClinicWard
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> Beds { get; set; }
        public Nullable<int> UsedBeds { get; set; }
        public Nullable<int> AvailableBeds { get; set; }
        public string Remark { get; set; }
    }
}
