using AppProtoype.iOS.CustomRenderer;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ListView), typeof(ListViewCustomRenderer))]
namespace AppProtoype.iOS.CustomRenderer
{
    public class ListViewCustomRenderer : ListViewRenderer
    {
        public ListViewCustomRenderer()
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
            var table = (UITableView)this.Control;
            table.SeparatorInset = UIEdgeInsets.Zero;
        }
    }
}
