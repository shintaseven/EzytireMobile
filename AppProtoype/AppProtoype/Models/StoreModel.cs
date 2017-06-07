using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProtoype.Models
{
    public struct StoreHours
    {
        public StoreHours(int opening, int closing, int openingMinutes = 0, int closingMinutes = 0)
        {
            Opening = new TimeSpan(opening, openingMinutes, 0);
            Closing = new TimeSpan(closing, closingMinutes, 0);
        }

        public TimeSpan Opening { get; set; }
        public TimeSpan Closing { get; set; }
    }
    public class StoreModel
    {
        public int StoreID { get; set; }
        public string StoreName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ContactNumber { get; set; }
        public string StoreImage { get; set; }
        public string StoreDescription { get; set; }
        public int GarageID { get; set; }
        public string BayID { get; set; }
        public int Latitude { get; set; }
        public int Longtitude { get; set; }
        public string Manager { get; set; }
        public List<StoreHours> OperatingHours { get; set; }
        public int RetailerID { get; set; }
        public string BayManagerAPIToken { get; set; }
        public List<string> ServiceTimeDisaplays { get; set; }
        public List<ServiceModel> AvailableServices { get; set; }
        public string AddressDisplay
        {
            get
            {
                return ZipCode + " " + Address1 + ", " + City + ", " + State; 
            }
        }
        public string PhoneDisplay
        {
            get
            {
                return "Phone: " + ContactNumber; 
            }
        }


    }
}
