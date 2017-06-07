using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace AppProtoype.ViewModels
{
    public class CouponViewModel : ViewCell
    {
        public static readonly BindableProperty CouponIDProperty =
            BindableProperty.Create("CouponID", typeof(int), typeof(CouponViewModel), 0);

        public int CouponID
        {
            get { return (int)GetValue(CouponIDProperty); }
            set { SetValue(CouponIDProperty, value); }
        }

        public static readonly BindableProperty ImageCaptionProperty =
            BindableProperty.Create("ImageCaption", typeof(string), typeof(CouponViewModel), "");

        public string ImageCaption
        {
            get { return (string)GetValue(ImageCaptionProperty); }
            set { SetValue(ImageCaptionProperty, value); }
        }

        public static readonly BindableProperty CodeProperty =
            BindableProperty.Create("Category", typeof(string), typeof(CouponViewModel), "");

        public string Category
        {
            get { return (string)GetValue(CodeProperty); }
            set { SetValue(CodeProperty, value); }
        }

        public static readonly BindableProperty ImageUrlInnerPageProperty =
            BindableProperty.Create("ImageUrlInnerPage", typeof(string), typeof(CouponViewModel), "");

        public string ImageUrlInnerPage
        {
            get { return (string)GetValue(ImageUrlInnerPageProperty); }
            set { SetValue(ImageUrlInnerPageProperty, value); }
        }

        public static readonly BindableProperty MoreDetailsProperty =
            BindableProperty.Create("MoreDetails", typeof(string), typeof(CouponViewModel), "");

        public string MoreDetails
        {
            get { return (string)GetValue(MoreDetailsProperty); }
            set { SetValue(MoreDetailsProperty, value); }
        }

    }
}
