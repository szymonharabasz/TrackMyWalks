using System;
using CoreLocation;
using TrackMyWalks.iOS.Services;
using TrackMyWalks.Services;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(WalkLocationService))]
namespace TrackMyWalks.iOS.Services
{
    public class Coordinates : EventArgs, IWalkLocationCoords
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class WalkLocationService : IWalkLocationService
    {
        CLLocationManager locationManager;
        CLLocation newLocation;

        public WalkLocationService()
        {
        }

        public event EventHandler<IWalkLocationCoords> MyLocation;

        public double GetDistanceTravelled(double lat, double lon)
        {
            var distance = newLocation.DistanceFrom(new CLLocation(lat, lon)) / 1000;
            return distance;
        }

        public void GetMyLocation()
        {
            locationManager = new CLLocationManager();
            if (CLLocationManager.LocationServicesEnabled)
            {
                locationManager.DesiredAccuracy = 1;
                if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
                {
                    locationManager.RequestAlwaysAuthorization();
                }
                if (UIDevice.CurrentDevice.CheckSystemVersion(9, 0))
                {
                    locationManager.AllowsBackgroundLocationUpdates = true;
                }
                locationManager.LocationsUpdated += (object sender, CLLocationsUpdatedEventArgs e) =>
                {
                    locationUpdated(e);
                };
                locationManager.AuthorizationChanged += (object sender, CLAuthorizationChangedEventArgs e) =>
                {
                    didAuthorizationChanged(e);
                    locationManager.RequestWhenInUseAuthorization();
                };
            }
        }

        public void didAuthorizationChanged(CLAuthorizationChangedEventArgs authStatus)
        {
            switch (authStatus.Status)
            {
                case CLAuthorizationStatus.AuthorizedAlways:
                    locationManager.RequestAlwaysAuthorization();
                    break;
                case CLAuthorizationStatus.AuthorizedWhenInUse:
                    locationManager.StartUpdatingLocation();
                    break;
                case CLAuthorizationStatus.Denied:
                    UIAlertView alert = new UIAlertView();
                    alert.Title = "Usługi lokalizacji są wyłączone";
                    alert.AddButton("OK");
                    alert.AddButton("Anuluj");
                    alert.Message = "Włącz lokalizację dla tej aplikacji w ustawieniach IPhone'a";
                    alert.AlertViewStyle = UIAlertViewStyle.Default;
                    alert.Show();
                    alert.Clicked += (object s, UIButtonEventArgs ev) =>
                    {
                        var Button = ev.ButtonIndex;
                    };
                    break;
                default:
                    break;

            }
        }

        public void locationUpdated(CLLocationsUpdatedEventArgs e)
        {
            var coords = new Coordinates();
            var locations = e.Locations;
            coords.latitude = locations[locations.Length - 1].Coordinate.Latitude;
            coords.longitude = locations[locations.Length - 1].Coordinate.Longitude;

            newLocation = new CLLocation(coords.latitude, coords.longitude);
            MyLocation(this, coords);
        }

        ~WalkLocationService()
        {
            locationManager.StopUpdatingLocation();
        }
    }
}
