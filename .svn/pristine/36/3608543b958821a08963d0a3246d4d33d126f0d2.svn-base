using AppProtoype.iOS.CustomRenderer;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(TabbedPageCustom))]
namespace AppProtoype.iOS.CustomRenderer
{
    public class TabbedPageCustom : TabbedRenderer
    {
        public TabbedPageCustom()
        {
            
            //TabBar.BackgroundColor = UIKit.UIColor.Black;
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                TabBar.TintColor = UIKit.UIColor.White;
                TabBar.BarTintColor = UIKit.UIColor.Black;
            }
        }
    }
}
