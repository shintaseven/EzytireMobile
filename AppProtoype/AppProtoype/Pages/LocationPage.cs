using AppProtoype.Models;
using AppProtoype.Services;
using AppProtoype.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppProtoype
{
    public class LocationPage : ContentPage
    {
        ListView contentListView;
        ActivityIndicator Indicator = new ActivityIndicator();

        public StoreModel CurrentStore { get; set; }

        public LocationPage(StoreModel currentStore = null)
        {
            StoreModelData storeCollection = new StoreModelData();
            List<StoreModel> sliderImages = storeCollection.StoreCollection.ToList();
            
            if (currentStore == null)
            {
                Title = "LOCATIONS";

                contentListView = new ListView { HasUnevenRows = true, BackgroundColor = Color.Transparent, SeparatorColor = Color.White, HorizontalOptions = LayoutOptions.FillAndExpand };

                contentListView.ItemSelected += OnItemSelected;
                contentListView.ItemTemplate = BuildCellViewDataTemplate();
                contentListView.ItemsSource = sliderImages;

                Content = new StackLayout
                {
                    Children = {
                        Indicator,
                        contentListView
                    }
                };
                Padding = new Thickness(0, 20, 0, 20);
            }
            else
            {
                CurrentStore = new StoreModel();
                CurrentStore = currentStore;

                ServiceModelData servicesData = new ServiceModelData();
                List<ServiceModel> currentServices = CurrentStore.AvailableServices;

                AbsoluteLayout layout = new AbsoluteLayout();
                if (CurrentStore != null)
                {
                    layout = BuildStoreInnerPage(CurrentStore, currentServices);
                }

                Grid servicesGrid = BuildServicesGrid(currentServices);
                Grid storeactions = BuildLocationActions(CurrentStore);
                Content = new StackLayout
                {
                    Children = {
                        layout,
                        servicesGrid,
                        storeactions
                    }
                };
            }
            
            BackgroundImage = "bg-inner.jpg";
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

                //nativeCell.SetBinding(StoreViewModel.Address1Property, "Address1");
                //nativeCell.SetBinding(StoreViewModel.CityProperty, "City");
                //nativeCell.SetBinding(StoreViewModel.StateProperty, "State");
                //nativeCell.SetBinding(StoreViewModel.ZipCodeProperty, "ZipCode");

                Label addressLabel = new Label();
                addressLabel.VerticalOptions = LayoutOptions.Start;
                addressLabel.HorizontalOptions = LayoutOptions.Start;
                addressLabel.SetBinding(Label.TextProperty, "AddressDisplay");
                addressLabel.FontSize = 12;
                addressLabel.TextColor = Color.FromHex("#A7A7A7");
                //addressLabel.HeightRequest = 50;
                Label contactLabel = new Label();

                Label addressLabel2 = new Label();
                addressLabel2.VerticalOptions = LayoutOptions.Start;
                addressLabel2.HorizontalOptions = LayoutOptions.Start;
                addressLabel2.SetBinding(Label.TextProperty, "PhoneDisplay");
                addressLabel2.FontSize = 12;
                addressLabel2.TextColor = Color.FromHex("#A7A7A7");
                //addressLabel.HeightRequest = 50;

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
                        addressLabel2
                    }
                };

                nativeCell.View = secondColStack;
                nativeCell.ForceUpdateSize();

                return nativeCell;

                });

            return couponItemTemplate;
        }

        private AbsoluteLayout BuildStoreInnerPage(StoreModel model, List<ServiceModel> currentServices)
        {
            AbsoluteLayout peakLayout = new AbsoluteLayout
            {
                HeightRequest = 250,
                BackgroundColor = Color.Black
            };

            var title = new Label
            {
                Text = model.StoreName,
                FontSize = 30,
                FontFamily = "AvenirNext-DemiBold",
                TextColor = Color.White
            };

            var where = new Label
            {
                Text = model.ZipCode + " " + model.Address1 + ", " + model.City + ", " + model.State,
                TextColor = Color.FromHex("#ddd"),
                FontFamily = "AvenirNextCondensed-Medium"
            };

            var image = new Image()
            {
                Source = ImageSource.FromFile(model.StoreImage),
                Aspect = Aspect.AspectFill,
            };

            var overlay = new BoxView()
            {
                Color = Color.Black.MultiplyAlpha(.7f)
            };

            var pin = new Image()
            {
                Source = ImageSource.FromFile("pin.png"),
                HeightRequest = 25,
                WidthRequest = 25
            };

            AbsoluteLayout.SetLayoutFlags(overlay, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(overlay, new Rectangle(0, 1, 1, 0.3));

            AbsoluteLayout.SetLayoutFlags(image, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(image, new Rectangle(0f, 0f, 1f, 1f));

            AbsoluteLayout.SetLayoutFlags(title, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(title,
                new Rectangle(0.1, 0.85, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize)
            );

            AbsoluteLayout.SetLayoutFlags(where, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(where,
                new Rectangle(0.1, 0.95, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize)
            );

            AbsoluteLayout.SetLayoutFlags(pin, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(pin,
                new Rectangle(0.95, 0.9, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize)
            );

            peakLayout.Children.Add(image);
            peakLayout.Children.Add(overlay);
            peakLayout.Children.Add(title);
            peakLayout.Children.Add(where);

            

            var secondColStack = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Children = {
                        peakLayout
                    }
                };


            return peakLayout;
        }

        private Grid BuildServicesGrid(List<ServiceModel> serviceCollection)
        {
            Grid servicesGrid = new Grid
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Transparent,
                //Padding = new Thickness(20, 10, 20, 0),
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }

                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };

            var firstColStack = new StackLayout
            {
                //Padding = new Thickness(20, 5, 5, 5),
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.Fill,

            };

            var secondColStack = new StackLayout
            {
                //Padding = new Thickness(20, 5, 5, 5),
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.Fill,

            };

            Label serviceTitleLabel = new Label();
            serviceTitleLabel.TextColor = Color.FromHex("#A7A7A7");
            serviceTitleLabel.Text = "Services: ";

            Label currentServiceLabel = new Label();
            foreach (ServiceModel service in serviceCollection)
            {
                currentServiceLabel.Text = " • " + service.ServiceName;
                currentServiceLabel.TextColor = Color.FromHex("#A7A7A7");
                if (serviceCollection.IndexOf(service) % 2 == 0)
                {
                    firstColStack.Children.Add(currentServiceLabel);
                }
                else
                {
                    secondColStack.Children.Add(currentServiceLabel);
                }
                currentServiceLabel = new Label();
            }

            servicesGrid.Children.Add(serviceTitleLabel, 0, 0);
            Grid.SetColumnSpan(serviceTitleLabel, 2);

            servicesGrid.Children.Add(firstColStack, 0, 1);

            servicesGrid.Children.Add(secondColStack, 1, 1);
            servicesGrid.Padding = new Thickness(20, 5, 5, 5);


            return servicesGrid;
        }

        private Grid BuildLocationActions(StoreModel currentStore)
        {

            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            Button callStoreButton = new Button() { Text = "CALL STORE" };
            callStoreButton.Image = "btncall.png";
            callStoreButton.Clicked += MakeACall;

            Button schedButton = new Button() { Text = "SCHEDULE APPOINTMENT" };
            schedButton.Image = "btnsched.png";
            schedButton.Clicked += GotoSchedApptment;

            Button getDirectionButton = new Button() { Text = "GET DIRECTIONS" };
            getDirectionButton.Image = "btnget.png";
            //Button faveButton = new Button() { Text = "MAKE THIS YOUR FAVORITE STORE" };
            //faveButton.Image = "btnmakefave.png";


            grid.Children.Add(callStoreButton, 0, 0);
            Grid.SetColumnSpan(callStoreButton, 2);
            grid.Children.Add(schedButton, 0, 1);
            Grid.SetColumnSpan(schedButton, 2);
            grid.Children.Add(getDirectionButton, 0, 2);
            Grid.SetColumnSpan(getDirectionButton, 2);
            //grid.Children.Add(faveButton, 0, 3);
            //Grid.SetColumnSpan(faveButton, 2);

            return grid;

        }

        private void GotoSchedApptment(object sender, EventArgs e)
        {
            AppointmentDetails appointment = new AppointmentDetails();
            appointment.SelectedStore = CurrentStore;
            appointment.SelectedServices = CurrentStore.AvailableServices;

            Navigation.PushAsync(new AppointmentPage(1, appointment));
        }

        private void MakeACall(object sender, EventArgs e)
        {
            //var url = new NSUrl("tel:121212");
            //App.SharedApplication.OpenUrl(url);

        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            // Deselect row
            contentListView.SelectedItem = null;

            Indicator.IsVisible = true;
            Indicator.Color = Color.White;
            Indicator.IsRunning = true;

            dynamic store = e.SelectedItem;

            store.AvailableServices = new List<ServiceModel>();
            //store.AvailableServices

            //await Navigation.PushAsync(new LocationPage(store));
            CurrentStore = store;
            Task task = GetServices(store);

            task.ContinueWith((t) =>
            {
                Indicator.IsVisible = false;
                Indicator.IsRunning = false;
                Navigation.PushAsync(new LocationPage(CurrentStore));

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

        private async Task GetServices(StoreModel store)
        {
            BayManagerService service = new BayManagerService();
            List<ServiceModel> services = await service.GetServicesByGarageID(store);
            CurrentStore.AvailableServices = services;


        }

    }
}
