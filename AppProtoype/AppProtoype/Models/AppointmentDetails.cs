using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProtoype.Models
{
    public class AppointmentDetails
    {
        public StoreModel SelectedStore { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public List<ServiceModel> SelectedServices {get; set;}
        public UserDetailsModel UserDetails { get; set; }
        public int Duration { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan FinishTime { get; set; }

        public AppointmentDetails()
        {
            SelectedStore = new StoreModel();
            SelectedServices = new List<ServiceModel>();
            UserDetails = new UserDetailsModel();
        }

    }
}
