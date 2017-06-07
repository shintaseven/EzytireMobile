using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace AppProtoype
{
    public class VehiclePage : ContentPage
    {
        public VehiclePage()
        {
            Title = "My Vehicle";
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello My Vehicle Page" }
                }
            };
        }
    }
}
