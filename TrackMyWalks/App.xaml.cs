using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using TrackMyWalks.Pages;

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
                var navPage = new NavigationPage(new TrackMyWalks.Pages.WalksPage())
                {
                    Title = "Track My Walks"
                };
                MainPage = navPage;

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
