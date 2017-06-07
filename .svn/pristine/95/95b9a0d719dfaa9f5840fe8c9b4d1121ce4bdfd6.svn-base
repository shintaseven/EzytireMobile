using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace AppProtoype.ViewModels
{
    public class StoreViewModel : ViewCell, INotifyPropertyChanged
    {

        public static readonly BindableProperty StoreIDProperty =
            BindableProperty.Create("StoreID", typeof(int), typeof(StoreViewModel), 0);

        public int StoreID
        {
            get { return (int)GetValue(StoreIDProperty); }
            set { SetValue(StoreIDProperty, value); }
        }

        public static readonly BindableProperty StoreNameProperty =
            BindableProperty.Create("StoreName", typeof(string), typeof(StoreViewModel), "");

        public string StoreName
        {
            get { return (string)GetValue(StoreNameProperty); }
            set { SetValue(StoreNameProperty, value); }
        }

        public static readonly BindableProperty Address1Property =
            BindableProperty.Create("Address1", typeof(string), typeof(StoreViewModel), "", propertyChanged: FormatAddress);

        private static void FormatAddress(BindableObject bindable, object oldValue, object newValue)
        {

        }

        public string Address1
        {
            get { return (string)GetValue(Address1Property); }
            set { SetValue(Address1Property, value); }
        }

        public static readonly BindableProperty AddressDisplayProperty =
            BindableProperty.Create("AddressDisplay", typeof(string), typeof(StoreViewModel), "");

        public string AddressDisplay
        {
            get { return (string)GetValue(AddressDisplayProperty); }
            set { SetValue(AddressDisplayProperty, value); }

        }

        public static readonly BindableProperty PhoneDisplayProperty =
            BindableProperty.Create("PhoneDisplay", typeof(string), typeof(StoreViewModel), "");

        public string PhoneDisplay
        {
            get { return (string)GetValue(PhoneDisplayProperty); }
            set { SetValue(PhoneDisplayProperty, value); }

        }

        public static readonly BindableProperty CityProperty =
            BindableProperty.Create("City", typeof(string), typeof(StoreViewModel), "");

        public string City
        {
            get { return (string)GetValue(CityProperty); }
            set { SetValue(CityProperty, value); }
        }

        public static readonly BindableProperty StateProperty =
            BindableProperty.Create("State", typeof(string), typeof(StoreViewModel), "");

        public string State
        {
            get { return (string)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        public static readonly BindableProperty ZipCodeProperty =
            BindableProperty.Create("ZipCode", typeof(string), typeof(StoreViewModel), "");

        public string ZipCode
        {
            get { return (string)GetValue(ZipCodeProperty); }
            set { SetValue(ZipCodeProperty, value); }
        }

        public static readonly BindableProperty ContactNumberProperty =
            BindableProperty.Create("ContactNumber", typeof(string), typeof(StoreViewModel), "");

        public string ContactNumber
        {
            get { return (string)GetValue(ContactNumberProperty); }
            set { SetValue(ContactNumberProperty, value); }
        }

        //protected override void OnBindingContextChanged()
        //{
        //    base.OnBindingContextChanged();

        //    if (BindingContext != null)
        //    {
        //        ForceUpdateSize();
        //    }
        //}

    }
}
