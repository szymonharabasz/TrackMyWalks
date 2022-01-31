using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using TrackMyWalks.Pages;
using TrackMyWalks.Services;
using TrackMyWalks.ViewModels;

namespace TrackMyWalks
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                MainPage = new SplashPage();
            }
            else
            {
                var walksPage = new NavigationPage(new WalksPage())
                {
                    Title = "Track My Walks - iOS"
                };
                walksPage.BarBackgroundColor = Color.FromHex("#440099");
                walksPage.BarTextColor = Color.White;
                var navService = DependencyService.Get<IWalkNavService>() as WalkNavService;
                navService.navigation = walksPage.Navigation;
                navService.RegisterViewMaping(typeof(WalksPageViewModel), typeof(WalksPage));
                navService.RegisterViewMaping(typeof(WalksEntryViewModel), typeof(WalkEntryPage));
                navService.RegisterViewMaping(typeof(WalksTrailViewModel), typeof(WalkTrailPage));
                navService.RegisterViewMaping(typeof(DistanceTravelledViewModel), typeof(DistanceTravelledPage));
                MainPage = walksPage;

            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
