using AppProtoype.Models;
using AppProtoype.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppProtoype.Views
{
    public partial class ImageSliderView : ContentView
    {
        public ImageSliderView()
        {
            InitializeComponent();

            HeightRequest = 200;


            DataTemplate sliderDataTemplate = BuildCellViewDataTemplate();

            CouponModelData sliderCollection = new CouponModelData();
            List<CouponModel> sliderImages = sliderCollection.SliderCollection.ToList();


            BuildSliderView(sliderDataTemplate, sliderImages);
        }

        private void BuildSliderView(DataTemplate dataTemplate, List<CouponModel> sliderImages)
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnTapGestureRecognizerTapped;

            //myCarousel object is declared on the xml page
            myCarousel.ItemsSource = sliderImages;
            myCarousel.ItemTemplate = dataTemplate;
            myCarousel.PageIndicators = true;
            myCarousel.Position = 0;
            myCarousel.PositionSelected += (sender, args) =>
            {
                dynamic carouselView = sender;
                dynamic currentCoupon = carouselView.ItemsSource[carouselView.Position - 1];
            };
            //myCarousel.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private DataTemplate BuildCellViewDataTemplate()
        {
            var tapGestureRecognizerForImage = new TapGestureRecognizer();
            tapGestureRecognizerForImage.Tapped += OnTapGestureRecognizerTappedForImage;

            DataTemplate sliderDataTemplate = new DataTemplate(() => {
                Grid grid = new Grid
                {
                    RowDefinitions =
                    {
                        new RowDefinition { Height = GridLength.Auto },
                        new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }

                    }
                };

                Image sliderImage = new Image();
                sliderImage.SetBinding(Image.SourceProperty, "ImageSliderUrl");
                sliderImage.SetBinding(Image.ClassIdProperty, "CouponID");
                //sliderImage.GestureRecognizers.Add(tapGestureRecognizerForImage);

                var mainContentStack = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand,

                    Children = {
                            sliderImage

                        }
                };

                CouponSliderViewModel couponViewModel = new CouponSliderViewModel();
                //couponViewModel.GestureRecognizers.Add(tapGestureRecognizerForImage);
                grid.Children.Add(sliderImage, 0, 0);
                grid.Children.Add(couponViewModel);
                Grid.SetRowSpan(sliderImage, 2);

                return grid;
            });

            return sliderDataTemplate;
        }

        async void OnTapGestureRecognizerTappedForImage(object sender, EventArgs e)
        {
            if (sender == null)
            {
                return;
            }

            dynamic carouselViewImage = sender;

            await Navigation.PushAsync(new OfferPage(carouselViewImage.ClassId));
        }

        async void OnTapGestureRecognizerTapped(object sender, EventArgs e)
        {

            if (sender == null)
            {
                return;
            }

            var zoo = e;
            if (zoo == null)
                return;


            dynamic carouselView = sender;
            dynamic currentCoupon = carouselView.ItemsSource[carouselView.Position - 1];

            await Navigation.PushAsync(new OfferPage(currentCoupon.CouponID));

        }
    }
}
