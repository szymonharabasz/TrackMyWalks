using System.Collections.ObjectModel;
using TrackMyWalks.Models;

namespace TrackMyWalks.ViewModels
{
    public class WalksPageViewModel : WalkBaseViewModel
    {
        ObservableCollection<WalkEntries> _walkEntries;

        public ObservableCollection<WalkEntries> walkEntries
        {
            get { return _walkEntries; }
            set { _walkEntries = value;
                OnPropertyChanged();
            }
        }

        public WalksPageViewModel()
        {
            walkEntries = new ObservableCollection<WalkEntries>
            {
                new WalkEntries
                {
                    Title  = "10-milowy szlak wzdłóż strumienia, Margaret River",
                    Notes  = "10-milowy szlak wzdłóż strumienia zaczyna się w Rotary Park w pobliżu Old Kate, czyli " +
                             "starej lokomotywy stojącej w północnej części Margaret River. ",
                    Latitude    = -33.9727604,
                    Longitude   = 115.0861599,
                    Kilometers  = 7.5,
                    Distance    = 0,
                    Difficulty  = "Średni",
                    ImageUrl    = "http://trailswa.com.au/media/cache/media/images/trails/_mid/" +
                                  "FullSizeRender1_600_480_c1.jpg"
                },
                new WalkEntries
                {
                    Title  = "Ścieżka Ancient Empire, Dolina Gigantów",
                    Notes  = "Ancient Empire to 450-metrowy szlak pośród " +
                             "gigantycznych drzew, wśród których znajdują się popularne sękate olbrzymy zwane " +
                             "Grandma Tingle.",
                    Latitude  = -34.9749188,
                    Longitude   = 117.3560796,
                    Kilometers = 450,
                    Distance   = 0,
                    Difficulty = "Trudny",
                    ImageUrl   = "http://trailswa.com.au/media/cache/media/images/trails/_mid/" +
                                 "Ancient_Empire_534_480_c1.jpg"
                }
            };
        }
    }
}
