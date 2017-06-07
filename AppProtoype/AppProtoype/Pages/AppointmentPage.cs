﻿using AppProtoype.Models;
using AppProtoype.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using XLabs.Forms.Controls;
using XLabs;
using System.ComponentModel;
using AppProtoype.Services;
using System.Threading.Tasks;

namespace AppProtoype
{
    public class AppointmentPage : ContentPage
    {
        ListView contentListView;
        int _currentStep = 1;

        public List<StoreModel> StoreModelList { get; set; }
        public List<string> CurrentSelectedServices { get; set; }
        public StoreModel CurrentStore { get; set; }
        public AppointmentDetails CurrentAppointment {get; set;}
        public bool ServiceSelected { get; set; }
        ImageButton ServiceNextButton = new ImageButton();
        ImageButton DateTimeNextButton = new ImageButton();
        ImageButton DetailsNextButton = new ImageButton();
        ActivityIndicator Indicator = new ActivityIndicator();
        Picker Timepicker = new Picker();
        Label NoSchedLabel = new Label();

        public AppointmentPage(int prevStep = 0, AppointmentDetails appointment = null)
        {
            if (Indicator.IsVisible)
            {
                Indicator.IsVisible = false;
            }

            _currentStep = ++prevStep;
            StoreModelData storeCollection = new StoreModelData();
            StoreModelList = new List<StoreModel>();
            StoreModelList = storeCollection.StoreCollection.ToList();

            Title = "SCHEDULE APPOINTMENT";

            Label stepTitle = new Label();
            stepTitle.VerticalOptions = LayoutOptions.Center;
            stepTitle.HorizontalOptions = LayoutOptions.Start;
            stepTitle.Text = GetStepLabelString();
            stepTitle.TextColor = Color.White;
            stepTitle.FontSize = 16;
            stepTitle.FontAttributes = FontAttributes.Bold;
            stepTitle.Margin = new Thickness(20, 20, 0, 0);

            CurrentAppointment = new AppointmentDetails();
            if (prevStep == 1)
            {

                contentListView = new ListView { HasUnevenRows = true, BackgroundColor = Color.Transparent, SeparatorColor = Color.White, HorizontalOptions = LayoutOptions.FillAndExpand };

                contentListView.ItemSelected += OnItemSelected;
                contentListView.ItemTemplate = BuildCellViewDataTemplate();
                contentListView.ItemsSource = StoreModelList;
                
                Content = new StackLayout
                {
                    Children = {
                        stepTitle,
                        Indicator,
                        contentListView
                    }
                };
            }
            else
            {
                if (appointment == null)
                {
                    appointment = new AppointmentDetails();
                }
                else
                {

                    CurrentAppointment = appointment;
                }

                switch (_currentStep)
                {
                    case 2: BuildStep2(stepTitle);
                        break;
                    case 3: BuildStep3(stepTitle);
                        break;
                    case 4: BuildStep4(stepTitle);
                        break;
                    case 5:
                        SaveUserDetails();
                        GoToStep5(stepTitle);
                        break;
                }
                 
            }
            Padding = new Thickness(0, 0, 0, 20);

            BackgroundImage = "bg-inner.jpg";
        }

        private async Task GetServices(StoreModel store)
        {
            BayManagerService service = new BayManagerService();
            List<ServiceModel> services = await service.GetServicesByGarageID(store);
            CurrentAppointment.SelectedStore.AvailableServices = services;


        }

        private async Task GetStoreScheds(DateTime dateSelected)
        {
            BayManagerService service = new BayManagerService();

            IEnumerable<int> serviceIDs = from ser in CurrentAppointment.SelectedServices
                                   select ser.ServiceID;

            List<string> serviceTimes = await service.GetStoreSchedule(CurrentAppointment, serviceIDs.ToList(), dateSelected);

            CurrentAppointment.SelectedStore.ServiceTimeDisaplays = serviceTimes;

        }

        private void BuildStep2(Label stepTitle)
        {
            CurrentSelectedServices = new List<string>();

            StackLayout currentSelectionsStack = BuildCurrentSelections();

            var headerAreaStack = new StackLayout
            {
                VerticalOptions = LayoutOptions.Start,
                BackgroundColor = Color.Black,
                Children = {
                        stepTitle,
                        currentSelectionsStack
                    }
            };

            StackLayout step2Stack = BuildStep2View();
            Content = new StackLayout
            {
                Children = {
                        headerAreaStack,
                        Indicator,
                        step2Stack
                    }
            };
        }

        private void BuildStep3(Label stepTitle)
        {
            StackLayout currentSelectionsStack = BuildCurrentSelections();

            var headerAreaStack = new StackLayout
            {
                VerticalOptions = LayoutOptions.Start,
                BackgroundColor = Color.Black,
                Children = {
                        stepTitle,
                        currentSelectionsStack
                    }
            };

            StackLayout step3Stack = BuildStep3View();

            Content = new StackLayout
            {
                Children = {
                        headerAreaStack,
                        Indicator,
                        step3Stack
                    }
            };
        }

        private void BuildStep4(Label stepTitle)
        {
            StackLayout currentSelectionsStack = BuildCurrentSelections();

            var headerAreaStack = new StackLayout
            {
                VerticalOptions = LayoutOptions.Start,
                BackgroundColor = Color.Black,
                Children = {
                        stepTitle,
                        currentSelectionsStack
                    }
            };

            StackLayout step4Stack = BuildStep4View();

            Content = new StackLayout
            {
                Children = {
                        headerAreaStack,
                        step4Stack
                    }
            };

        }

        private void GoToStep5(Label stepTitle)
        {
            StackLayout currentSelectionsStack = BuildCurrentSelections();
            currentSelectionsStack.BackgroundColor = Color.Transparent;
            var headerAreaStack = new StackLayout
            {
                VerticalOptions = LayoutOptions.Start,
                Children = {
                        stepTitle,
                        currentSelectionsStack
                    }
            };

            StackLayout step5Stack = BuildStep5View();
            step5Stack.Margin = new Thickness(20, 0, 0, 10);

            Content = new StackLayout
            {
                Children = {
                        headerAreaStack,
                        step5Stack
                    }
            };

        }

        private DataTemplate BuildCellViewDataTemplate()
        {
            DataTemplate couponItemTemplate = new DataTemplate(() => {
                var nativeCell = new StoreViewModel();


                Label storeTitleLabel = new Label();
                storeTitleLabel.VerticalOptions = LayoutOptions.Center;
                storeTitleLabel.SetBinding(Label.TextProperty, "StoreName");
                storeTitleLabel.FontSize = 14;

                Image arrowImage = new Image();
                arrowImage.Source = "offerarwbg.png";

                nativeCell.SetBinding(StoreViewModel.Address1Property, "Address1");
                nativeCell.SetBinding(StoreViewModel.CityProperty, "City");
                nativeCell.SetBinding(StoreViewModel.StateProperty, "State");
                nativeCell.SetBinding(StoreViewModel.ZipCodeProperty, "ZipCode");

                Label addressLabel = new Label();
                addressLabel.VerticalOptions = LayoutOptions.Start;
                addressLabel.HorizontalOptions = LayoutOptions.Start;
                addressLabel.SetBinding(Label.TextProperty, "AddressDisplay");
                addressLabel.FontSize = 12;
                addressLabel.TextColor = Color.FromHex("#A7A7A7");
                //addressLabel.HeightRequest = 50;
                Label contactLabel = new Label();

                Label phoneDisplay = new Label();
                phoneDisplay.VerticalOptions = LayoutOptions.Start;
                phoneDisplay.HorizontalOptions = LayoutOptions.Start;
                phoneDisplay.SetBinding(Label.TextProperty, "PhoneDisplay");
                phoneDisplay.FontSize = 12;
                phoneDisplay.TextColor = Color.FromHex("#A7A7A7");


                RelativeLayout layout = new RelativeLayout();
                //layout.BackgroundColor = Color.Blue;
                layout.Children.Add(arrowImage,
                    Constraint.Constant(0),
                    Constraint.Constant(0),
                    Constraint.RelativeToParent((parent) => { return parent.Width - 50; }),
                    Constraint.RelativeToParent((parent) => { return parent.Height; }));

                layout.Children.Add(storeTitleLabel,
                    Constraint.Constant(0),
                    Constraint.Constant(0),
                    Constraint.RelativeToParent((parent) => { return parent.Width; }),
                    Constraint.RelativeToParent((parent) => { return parent.Height; }));

                var secondColStack = new StackLayout
                {
                    Padding = new Thickness(20, 5, 5, 5),
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Children = {
                        layout,
                        addressLabel,
                        phoneDisplay
                    }
                };

                nativeCell.View = secondColStack;
                nativeCell.ForceUpdateSize();

                return nativeCell;

            });

            return couponItemTemplate;
        }

        private StackLayout BuildStep2View()
        {
            var servicesStack = new StackLayout
            {
                Padding = new Thickness(10, 10, 0, 0),
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            ServiceModelData servicesCollection = new ServiceModelData();
            List<ServiceModel> services = CurrentAppointment.SelectedStore.AvailableServices;
            foreach (ServiceModel service in services)
            {
                CheckBox cbox = new CheckBox();
                cbox.DefaultText = service.ServiceName;
                cbox.TextColor = Color.White;
                cbox.StyleId = service.ServiceID.ToString();
                cbox.CheckedChanged += OnCheckboxSelected;

                servicesStack.Children.Add(cbox);
            }

            ServiceNextButton.BackgroundColor = Color.Yellow;
            ServiceNextButton.Text = "NEXT";
            ServiceNextButton.TextColor = Color.Black;
            ServiceNextButton.Margin = 50;
            ServiceNextButton.IsEnabled = false;
            ServiceNextButton.Clicked += GoToStep3;

            var mainContentStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                
                Children = {
                    servicesStack,
                    ServiceNextButton
                    }
            };

            return mainContentStack;
        }

        private StackLayout BuildStep3View()
        {
            CalendarView calendar = new CalendarView();
            calendar.HorizontalOptions = LayoutOptions.CenterAndExpand;
            calendar.VerticalOptions = LayoutOptions.Center;
            calendar.SelectedDate = DateTime.Now;
            calendar.MinDate = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, 1);
            calendar.MaxDate = new DateTime(2020, 12, 31);
            calendar.Margin = new Thickness(0, 40, 0, 40);
            calendar.Scale = 1.25;
            calendar.DateSelected += SaveDateSelected;

            //TimePicker timePicker = new TimePicker();
            //timePicker.Time = new TimeSpan(8, 0, 0);
            //timePicker.HorizontalOptions = LayoutOptions.FillAndExpand;


            Timepicker = new Picker
            {
                Title = "Select Time",
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            foreach(string serviceSched in CurrentAppointment.SelectedStore.ServiceTimeDisaplays)
            {
                Timepicker.Items.Add(serviceSched);
            }
            Timepicker.SelectedIndex = 0;


            DateTime tempDate = DateTime.Now.Date;
            TimeSpan convertedTime = new TimeSpan(8, 0, 0);

            CurrentAppointment.AppointmentDate = tempDate + convertedTime;
            Timepicker.PropertyChanged += SaveTimeSelected;

            DateTimeNextButton.BackgroundColor = Color.Yellow;
            DateTimeNextButton.Text = "NEXT";
            DateTimeNextButton.TextColor = Color.Black;
            DateTimeNextButton.Clicked += GoToStep4;

            var mainContentStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,

                Children = {
                    calendar,
                    Timepicker,
                    NoSchedLabel,
                    DateTimeNextButton
                    }
            };

            return mainContentStack;

        }

        private StackLayout BuildStep4View()
        {
            Label personalDetailsLabel = new Label();
            personalDetailsLabel.Text = "Personal Details";
            personalDetailsLabel.TextColor = Color.White;
            personalDetailsLabel.FontSize = 16;

            UserDetailsModel model = App.Database.GetCurrentUser();
            if (model != null)
            {
                CurrentAppointment.UserDetails = model;
            }

            Entry nameTextBox = new Entry() { Placeholder = "Full Name" };
            nameTextBox.BindingContext = CurrentAppointment.UserDetails;
            nameTextBox.SetBinding(Entry.TextProperty, "FullName");

            Entry addressLine1TextBox = new Entry() { Placeholder = "Address Line" };
            addressLine1TextBox.BindingContext = CurrentAppointment.UserDetails;
            addressLine1TextBox.SetBinding(Entry.TextProperty, "Addressline");

            Entry cityTextBox = new Entry() { Placeholder = "City" };
            cityTextBox.BindingContext = CurrentAppointment.UserDetails;
            cityTextBox.SetBinding(Entry.TextProperty, "City");

            Entry stateProvRegionTextBox = new Entry() { Placeholder = "State/Province/Region" };
            stateProvRegionTextBox.BindingContext = CurrentAppointment.UserDetails;
            stateProvRegionTextBox.SetBinding(Entry.TextProperty, "StateCityRegion");

            Entry zipTextBox = new Entry() { Placeholder = "Zip" };
            zipTextBox.BindingContext = CurrentAppointment.UserDetails;
            zipTextBox.SetBinding(Entry.TextProperty, "Zip");

            Entry phoneTextBox = new Entry() { Placeholder = "Contact No." };
            phoneTextBox.BindingContext = CurrentAppointment.UserDetails;
            phoneTextBox.SetBinding(Entry.TextProperty, "ContactNumber");

            var personalDetailsStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,

                Children = {
                            personalDetailsLabel,
                            nameTextBox,
                            addressLine1TextBox,
                            cityTextBox,
                            stateProvRegionTextBox,
                            zipTextBox,
                            phoneTextBox
                        }
            };



            Label vehiclesDetailsLabel = new Label();
            vehiclesDetailsLabel.Text = "Vehicle Details";
            vehiclesDetailsLabel.TextColor = Color.White;
            vehiclesDetailsLabel.FontSize = 16;

            Entry vehicleYearTextBox = new Entry() { Placeholder = "Year" };
            vehicleYearTextBox.BindingContext = CurrentAppointment.UserDetails;
            vehicleYearTextBox.SetBinding(Entry.TextProperty, "VehicleYear");

            Entry vehicleMakeTextBox = new Entry() { Placeholder = "Make" };
            vehicleMakeTextBox.BindingContext = CurrentAppointment.UserDetails;
            vehicleMakeTextBox.SetBinding(Entry.TextProperty, "VehicleMake");

            Entry vehicleModelTextBox = new Entry() { Placeholder = "Model" };
            vehicleModelTextBox.BindingContext = CurrentAppointment.UserDetails;
            vehicleModelTextBox.SetBinding(Entry.TextProperty, "VehicleModel");

            Entry vehicleOptionTextBox = new Entry() { Placeholder = "Option" };
            vehicleOptionTextBox.BindingContext = CurrentAppointment.UserDetails;
            vehicleOptionTextBox.SetBinding(Entry.TextProperty, "VehicleOption");

            var vehicleDetailsStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,

                Children = {
                            vehiclesDetailsLabel,
                            vehicleYearTextBox,
                            vehicleMakeTextBox,
                            vehicleModelTextBox,
                            vehicleOptionTextBox
                        }
            };

            DetailsNextButton.BackgroundColor = Color.Yellow;
            DetailsNextButton.Text = "NEXT";
            DetailsNextButton.TextColor = Color.Black;
            DetailsNextButton.Margin = 20;
            DetailsNextButton.Clicked += GoToStep5;


            var mainContentStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(20, 0, 20, 0),

                Children = {
                            personalDetailsStack,
                            vehicleDetailsStack,
                            DetailsNextButton
                        }
            };

            return mainContentStack;
        }

        private StackLayout BuildStep5View()
        {
            Label personalDetailsLabel = new Label();
            personalDetailsLabel.Text = "Personal Details: ";
            personalDetailsLabel.FontAttributes = FontAttributes.Bold;
            personalDetailsLabel.TextColor = Color.White;

            Label nameLabel = new Label();
            nameLabel.Text = CurrentAppointment.UserDetails.FullName;
            nameLabel.TextColor = Color.White;

            Label addressLabel = new Label();
            string fullAdd = CurrentAppointment.UserDetails.Zip + " " + CurrentAppointment.UserDetails.Addressline +", " + CurrentAppointment.UserDetails.City + ", " + CurrentAppointment.UserDetails.StateCityRegion;
            addressLabel.Text = fullAdd;
            addressLabel.TextColor = Color.White;


            Label contactLabel = new Label();
            contactLabel.Text = CurrentAppointment.UserDetails.ContactNumber;
            contactLabel.TextColor = Color.White;

            var personalDetailsStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(0, 0, 0, 20),
                Children = {
                            personalDetailsLabel,
                            nameLabel,
                            addressLabel,
                            contactLabel

                        }
            };

            Label vehicleLabel = new Label();
            vehicleLabel.Text = "Vehicle Details: ";
            vehicleLabel.FontAttributes = FontAttributes.Bold; 
            vehicleLabel.TextColor = Color.White;

            string fullVehicle = CurrentAppointment.UserDetails.VehicleYear + " " + CurrentAppointment.UserDetails.VehicleMake + " " + CurrentAppointment.UserDetails.VehicleModel + " " + CurrentAppointment.UserDetails.VehicleOption;

            Label customerVehicle = new Label();
            customerVehicle.Text = fullVehicle;
            customerVehicle.TextColor = Color.White;

            
            var vehicleDetailsStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(0, 0, 0, 0),
                Children = {
                            vehicleLabel,
                            customerVehicle

                        }
            };

            ImageButton submitButton = new ImageButton();

            submitButton.BackgroundColor = Color.Yellow;
            submitButton.Text = "SUBMIT";
            submitButton.TextColor = Color.Black;
            submitButton.Margin = 30;
            submitButton.Clicked += SubmitAppointmentRequest;

            var mainContentStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,

                Children = {
                            personalDetailsStack,
                            vehicleDetailsStack,
                            submitButton

                        }
            };


            return mainContentStack;
        }

        async void SubmitAppointmentRequest(object sender, EventArgs e)
        {
            BayManagerService service = new BayManagerService();

            //await DisplayAlert("", "Request Submitted", "OK");

            await service.BookAppointmentSchedule(CurrentAppointment);

            //await Navigation.PopAsync();
        }

        private void SaveTimeSelected(object sender, PropertyChangedEventArgs e)
        {
            var selectedTime = (Picker)sender;
            //DateTime tempDate = CurrentAppointment.AppointmentDate.Date;

            //StoreHours storeHours = CurrentAppointment.SelectedStore.OperatingHours.First();

            //if (selectedTime.Time.Hours < storeHours.Opening.Hours || selectedTime.Time.Hours > storeHours.Closing.Hours)
            //{
            //    DateTimeNextButton.IsEnabled = false;
            //}
            //else
            //{
            //    DateTimeNextButton.IsEnabled = true;
            //}

            //TimeSpan convertedTime = new TimeSpan(selectedTime.Time.Hours, selectedTime.Time.Minutes, 0);
            //CurrentAppointment.AppointmentDate = tempDate + convertedTime;
            if (selectedTime.SelectedIndex >= 0)
            {
                CurrentAppointment.AppointmentTime = selectedTime.Items[selectedTime.SelectedIndex];
            }
        }

        private void SaveDateSelected(object sender, DateTime e)
        {
            ActivateIndicator();

            var selectedDate = (CalendarView)sender;
            DateTime currentDate = (DateTime)selectedDate.SelectedDate;

            Task task = GetStoreScheds(currentDate);

            task.ContinueWith((t) =>
            {
                DisplayServiceTimesOption();
                DisableIndicator();

            }, TaskScheduler.FromCurrentSynchronizationContext());

            if (currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday || (currentDate.Month == DateTime.Today.Month && currentDate.Day < DateTime.Today.Day))
            {
                DateTimeNextButton.IsEnabled = false;
            }
            else
            {
                DateTimeNextButton.IsEnabled = true;
            }

            //CurrentAppointment.AppointmentTime = e.Date + new TimeSpan(CurrentAppointment.AppointmentTime.Hour, CurrentAppointment.AppointmentTime.Minute, 0);
            CurrentAppointment.AppointmentDate = e.Date;
        }

        private void DisplayServiceTimesOption()
        {
            if (CurrentAppointment.SelectedStore.ServiceTimeDisaplays.Count == 0)
            {
                Timepicker.IsVisible = false;
                NoSchedLabel.IsVisible = true;
                NoSchedLabel.Text = "No Schedule available for this date";
                NoSchedLabel.TextColor = Color.White;
            }
            else
            {
                Timepicker.Items.Clear();
                foreach (string serviceSched in CurrentAppointment.SelectedStore.ServiceTimeDisaplays)
                {
                    Timepicker.Items.Add(serviceSched);
                }
                

                Timepicker.IsVisible = true;
                Timepicker.SelectedIndex = 1;
                NoSchedLabel.IsVisible = false;
            }

            DisableIndicator();
        }

        private StackLayout BuildCurrentSelections()
        {
            StoreModel currentStore = CurrentAppointment.SelectedStore;
            Label currentSelections = new Label();
            string currentStoreText = string.Empty;
            string currentServiceText = string.Empty;
            string currentDateTimeText = string.Empty;

            Label locationTitle = new Label();
            locationTitle.FontAttributes = FontAttributes.Bold;
            locationTitle.Text = "Store Location:";
            locationTitle.TextColor = Color.White;
            locationTitle.FontSize = 16;

            Label locationValue = new Label();
            Label servicesValue = new Label();
            Label dateTimeValue = new Label();

            Label serviceTitle = new Label();
            serviceTitle.FontAttributes = FontAttributes.Bold;
            serviceTitle.Text = "Service/s:";
            serviceTitle.TextColor = Color.White;
            serviceTitle.FontSize = 16;

            Label dateTimeTitle = new Label();
            dateTimeTitle.FontAttributes = FontAttributes.Bold;
            dateTimeTitle.Text = "Date and Time:";
            dateTimeTitle.TextColor = Color.White;
            dateTimeTitle.FontSize = 16;

            var selectionsStack = new StackLayout
            {
                VerticalOptions = LayoutOptions.Start,
                BackgroundColor = Color.Black,
                Margin = new Thickness(20, 0, 0, 10)
            };

            switch (_currentStep)
            {
                case 2:
                    currentStoreText =  currentStore.StoreName + "\n"
                                        + currentStore.AddressDisplay;

                    locationValue.Text = currentStoreText;
                    locationValue.VerticalOptions = LayoutOptions.Center;
                    locationValue.TextColor = Color.White;
                    locationValue.FontSize = 16;

                    selectionsStack.Children.Add(locationTitle);
                    selectionsStack.Children.Add(locationValue);

                    break;

                case 3:
                    currentStoreText = currentStore.StoreName + "\n"
                                        + currentStore.AddressDisplay;

                    locationValue.Text = currentStoreText;
                    locationValue.VerticalOptions = LayoutOptions.Center;
                    locationValue.TextColor = Color.White;
                    locationValue.FontSize = 16;

                    foreach(ServiceModel service in CurrentAppointment.SelectedServices)
                    {
                        currentServiceText += service.ServiceName;

                        if (service != CurrentAppointment.SelectedServices.Last())
                        {
                            currentServiceText += ", ";
                        }
                    }

                    servicesValue.Text = currentServiceText;
                    servicesValue.VerticalOptions = LayoutOptions.Center;
                    servicesValue.TextColor = Color.White;
                    servicesValue.FontSize = 16;

                    selectionsStack.Children.Add(locationTitle);
                    selectionsStack.Children.Add(locationValue);

                    selectionsStack.Children.Add(serviceTitle);
                    selectionsStack.Children.Add(servicesValue);

                    break;
                case 5:
                    currentStoreText = currentStore.StoreName + "\n"
                                        + currentStore.AddressDisplay;

                    locationValue.Text = currentStoreText;
                    locationValue.VerticalOptions = LayoutOptions.Center;
                    locationValue.TextColor = Color.White;
                    locationValue.FontSize = 16;

                    
                    foreach (ServiceModel service in CurrentAppointment.SelectedServices)
                    {
                        currentServiceText += service.ServiceName;

                        if (service != CurrentAppointment.SelectedServices.Last())
                        {
                            currentServiceText += ", ";
                        }
                    }

                    servicesValue.Text = currentServiceText;
                    servicesValue.VerticalOptions = LayoutOptions.Center;
                    servicesValue.TextColor = Color.White;
                    servicesValue.FontSize = 16;

                    currentDateTimeText += CurrentAppointment.AppointmentDate.ToString("d");
                    currentDateTimeText += " " + CurrentAppointment.AppointmentTime;

                    dateTimeValue.VerticalOptions = LayoutOptions.Center;
                    dateTimeValue.TextColor = Color.White;
                    dateTimeValue.FontSize = 16;
                    dateTimeValue.Text = currentDateTimeText;

                    selectionsStack.Children.Add(locationTitle);
                    selectionsStack.Children.Add(locationValue);

                    selectionsStack.Children.Add(serviceTitle);
                    selectionsStack.Children.Add(servicesValue);

                    selectionsStack.Children.Add(dateTimeTitle);
                    selectionsStack.Children.Add(dateTimeValue);


                    break;
            }

            return selectionsStack;
        }

        private string GetStepLabelString()
        {
            switch (_currentStep)
            {
                case 1: return "SELECT LOCATION";
                case 2: return "SELECT SERVICES";
                case 3: return "SELECT APPOINTMENT DATE AND TIME";
                case 4: return "ENTER ADDITIONAL DETAILS";
                case 5: return "CONFIRM DETAILS";
                default: return "";
            }
        }

        private void GoToStep3(object sender, EventArgs e)
        {
            Indicator.IsVisible = true;
            Indicator.Color = Color.White;
            Indicator.IsRunning = true;
            ServiceNextButton.IsEnabled = false;

            IEnumerable<ServiceModel> selectedServicesObject = from ser in CurrentAppointment.SelectedStore.AvailableServices
                                                         where CurrentSelectedServices.Contains(ser.ServiceID.ToString())
                                                         select ser;

            CurrentAppointment.SelectedServices = selectedServicesObject.ToList();

            Task task = GetStoreScheds(DateTime.Now);

            task.ContinueWith(async (t) =>
            {
                Indicator.IsVisible = false;
                Indicator.IsRunning = false;
                ServiceNextButton.IsEnabled = true;
                await Navigation.PushAsync(new AppointmentPage(_currentStep, CurrentAppointment));

            }, TaskScheduler.FromCurrentSynchronizationContext());

       }


        async private void GoToStep4(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AppointmentPage(_currentStep, CurrentAppointment));
        }

        async void GoToStep5(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AppointmentPage(_currentStep, CurrentAppointment));

        }

        private void OnCheckboxSelected(object sender, EventArgs<bool> e)
        {
            CheckBox isCheckedOrNot = (CheckBox)sender;
            if (isCheckedOrNot.Checked)
            {
                if (!CurrentSelectedServices.Contains(isCheckedOrNot.StyleId))
                {
                    CurrentSelectedServices.Add(isCheckedOrNot.StyleId);
                }
            }
            else
            {
                if (CurrentSelectedServices.Contains(isCheckedOrNot.StyleId))
                {
                    CurrentSelectedServices.Remove(isCheckedOrNot.StyleId);
                }
            }

            if (CurrentSelectedServices.Count > 0)
            {
                ServiceNextButton.IsEnabled = true;
            }
            else
            {
                ServiceNextButton.IsEnabled = false;
            }
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            Indicator.IsVisible = true;
            Indicator.Color = Color.White;
            Indicator.IsRunning = true;

            // Deselect row
            contentListView.SelectedItem = null;

            dynamic store = e.SelectedItem;
            CurrentAppointment.SelectedStore = store;

            Task task = GetServices(CurrentAppointment.SelectedStore);

            task.ContinueWith((t) =>
            {
                Indicator.IsVisible = false;
                Indicator.IsRunning = false;
                Navigation.PushAsync(new AppointmentPage(_currentStep, CurrentAppointment));

            }, TaskScheduler.FromCurrentSynchronizationContext());

           

        }

        private void ActivateIndicator()
        {
            Indicator.IsVisible = true;
            Indicator.IsRunning = true;
            Indicator.Color = Color.White;
        }

        private void DisableIndicator()
        {
            Indicator.IsVisible = false;
            Indicator.IsRunning = false;
        }

        private void SaveUserDetails()
        {
            UserDetailsModel user = new UserDetailsModel();
            
            App.Database.SaveUserDetails(user);
            //UserDetailsModel model = App.Database.GetCurrentUser();
        }
    }
}
