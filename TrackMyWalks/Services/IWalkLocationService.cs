using System;
namespace TrackMyWalks.Services
{
    public interface IWalkLocationService
    {
        void GetMyLocation();

        double GetDistanceTravelled(double lat, double lon);
        event EventHandler<IWalkLocationCoords> MyLocation;
    }

    public interface IWalkLocationCoords
    {
        double latitude { get; set; }
        double longitude { get; set; }
    }
}
