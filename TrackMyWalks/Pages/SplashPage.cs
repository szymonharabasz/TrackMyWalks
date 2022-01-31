using System;
using System.Threading.Tasks;
using TrackMyWalks.Services;
using TrackMyWalks.ViewModels;
using Xamarin.Forms;

namespace TrackMyWalks.Pages
{
    public class SplashPage : ContentPage
    {
        public SplashPage()
        {
            AbsoluteLayout splashLayout = new AbsoluteLayout
            {
                HeightRequest = 600
            };
            var image = new Image()
            {
                Source = ImageSource.FromFile("icon.png"),
                Aspect = Aspect.AspectFill
            };
            AbsoluteLayout.SetLayoutFlags(image, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(image, new Rectangle(0f, 0f, 1f, 1f));
            splashLayout.Children.Add(image);

            Content = new StackLayout
            {
       
                Children = {
                    splashLayout
                }
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(3000);

            var navPage = new NavigationPage(new WalksPage()
            {
                Title = "Track My Walks - Android"
            });
            navPage.BarBackgroundColor = Color.FromHex("#4C5678");
            navPage.BarTextColor = Color.White;
            var navService = DependencyService.Get<IWalkNavService>() as WalkNavService;
            navService.navigation = navPage.Navigation;
            navService.RegisterViewMaping(typeof(WalksPageViewModel), typeof(WalksPage));
            navService.RegisterViewMaping(typeof(WalksEntryViewModel), typeof(WalkEntryPage));
            navService.RegisterViewMaping(typeof(WalksTrailViewModel), typeof(WalkTrailPage));
            navService.RegisterViewMaping(typeof(DistanceTravelledViewModel), typeof(DistanceTravelledPage));

            Application.Current.MainPage = navPage;
        }
    }
}

