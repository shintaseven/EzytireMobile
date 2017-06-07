using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProtoype.Models
{
    [Table("UserDetail")]
    public class UserDetailsModel
    {
        [PrimaryKey, AutoIncrement]
        public int UserID  { get; set; }

        public string FullName { get; set; }
        public string Addressline { get; set; }
        public string City { get; set; }
        public string StateCityRegion { get; set; }
        public string Zip { get; set; }
        public string ContactNumber { get; set; }

        public string VehicleYear { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleOption { get; set; }
    }
}
