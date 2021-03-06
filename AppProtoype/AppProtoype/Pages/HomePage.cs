﻿using AppProtoype.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace AppProtoype
{
    public class HomePage : ContentPage
    {
        public HomePage()
        {
            Title = "HOME";
            //Icon = "home01.png";
            Content = new StackLayout
            {
                Children = {
                    new Label {
                        Text = "VEHICLE SERVICE AT YOUR FINGERTIPS",
                        TextColor = Color.White,
                        FontSize =  Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                        FontAttributes = FontAttributes.Bold,
                        HorizontalTextAlignment = TextAlignment.Center
                    },
                    new Label {
                        Text = "Ezytire is putting our customers at the fore front of technology; by offering clients to opportunity to provide their customers to connect with them seemlessly through their mobile devices.",
                        TextColor = Color.FromHex("DDDDDD"),
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontSize =  Device.GetNamedSize(NamedSize.Small, typeof(Label))
                    },
                    new ImageSliderView()
                }
            };

            Padding = new Thickness(20, Device.OnPlatform(40, 20, 20), 20, 20);
            BackgroundImage = "bg-tire.jpg";
        }
    }
}
