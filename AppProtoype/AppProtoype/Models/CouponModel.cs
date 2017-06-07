
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppProtoype.Shared;
using Xamarin.Forms;

namespace AppProtoype.Models
{
    public class CouponModel
    {
        public int CouponID { get; set; }
        public string Title { get; set; }
        public CouponCategoryEnum Category { get; set; }
        public string CategoryImage { get; set; }
        public string ImageSliderUrl { get; set; }
        //public string ImageForInnerPage { get; set; }
        public string ImageCaption { get; set; }
        public string Code { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string MoreDetails { get; set; }
        public ImageSource ImageForInnerPage { get; set;}
    }
}
