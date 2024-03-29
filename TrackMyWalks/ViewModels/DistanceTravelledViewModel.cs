﻿using System.Threading.Tasks;
using TrackMyWalks.Models;
using TrackMyWalks.Services;
using Xamarin.Forms;

namespace TrackMyWalks.ViewModels
{
    public class DistanceTravelledViewModel : WalkBaseViewModel<WalkEntries>
    {
        IWalkLocationService locationService;

        WalkEntries _walkEntry;
        public WalkEntries WalkEntry
        {
            get { return _walkEntry; }
            set {
                _walkEntry = value;
                OnPropertyChanged();
            }
        }

        double _travelled;
        public double Travelled
        {
            get { return _travelled; }
            set
            {
                _travelled = value;
                OnPropertyChanged();
            }
        }
        double _hours;
        public double Hours
        {
            get { return _hours; }
            set {
                _hours = value;
                OnPropertyChanged();
            }
        }
        double _minutes;
        public double Minutes
        {
            get { return _minutes; }
            set {
                _minutes = value;
                OnPropertyChanged();
            }
        }
        double _seconds;
        public double Seconds
        {
            get { return _seconds; }
            set
            {
                _seconds = value;
                OnPropertyChanged();
            }
        }
        public string TimeTaken
        {
            get
            {
                return string.Format("{0:00}:{1:00}:{2:00}", this.Hours, this.Minutes, this.Seconds);
            }
        }

        public DistanceTravelledViewModel(IWalkNavService navService) : base(navService)
        {
            this.Travelled = 100;
            this.Hours = 0;
            this.Minutes = 0;
            this.Seconds = 0;

            locationService = DependencyService.Get<IWalkLocationService>();
            locationService.MyLocation += (object sender, IWalkLocationCoords e) =>
            {
                if (_walkEntry != null)
                {
                    var distance = locationService.GetDistanceTravelled(_walkEntry.Latitude, _walkEntry.Longitude);
                    this.Travelled = distance;
                }
            };
            locationService.GetMyLocation();
        }

        public override async Task Init(WalkEntries walkDetails)
        {
            await Task.Factory.StartNew(() =>
            {
                WalkEntry = walkDetails;
            });
        }

        Command _mainPage;
        public Command BackToMainPage
        {
            get
            {
                return _mainPage ?? (_mainPage = new Command(async () =>
                {
                    await NavService.BackToMainPage();
                }));
            }
        }
    }
}
