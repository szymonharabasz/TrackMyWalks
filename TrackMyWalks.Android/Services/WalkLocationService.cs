using System;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using TrackMyWalks.Droid.Services;
using TrackMyWalks.Services;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(WalkLocationService))]
namespace TrackMyWalks.Droid.Services
{
    public class Coordinates : EventArgs, IWalkLocationCoords
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class WalkLocationService : Java.Lang.Object, IWalkLocationService, ILocationListener
    {
        LocationManager locationManager;
        Location newLocation;

        public WalkLocationService()
        {
            locationManager = (LocationManager)Android.App.Application.Context.GetSystemService(Context.LocationService);
        }

        public event EventHandler<IWalkLocationCoords> MyLocation;

        public double GetDistanceTravelled(double lat, double lon)
        {
            Location locationB = new Location("Koniec szlaku");
            locationB.Latitude = lat;
            locationB.Longitude = lon;
            float distance = newLocation.DistanceTo(locationB) / 1000;
            return distance;
        }

        [Obsolete]
        public void GetMyLocation()
        {
            // TODO: obtain locationManager
            long minTime = 0;
            float minDistance = 0;
            Forms.Context.GetSystemService(Context.LocationService);
            locationManager.RequestLocationUpdates(
                LocationManager.NetworkProvider,
                minTime, minDistance, this);
        }

        public void OnLocationChanged(Location location)
        {
            if (location != null)
            {
                var coords = new Coordinates
                {
                    latitude = location.Latitude,
                    longitude = location.Longitude
                };
                newLocation = new Location("Punkt A");
                newLocation.Latitude = coords.latitude;
                newLocation.Longitude = coords.longitude;

                MyLocation(this, coords);
            }
        }

        public void OnProviderDisabled(string provider) { }
        public void OnProviderEnabled(string provider) { }
        public void OnStatusChanged(string provider, Availability status, Bundle extras) { }

        ~WalkLocationService()
        {
            locationManager.RemoveUpdates(this);
        }
    }
}
