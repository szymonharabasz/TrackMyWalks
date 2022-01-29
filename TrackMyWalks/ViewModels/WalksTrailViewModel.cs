using TrackMyWalks.Models;

namespace TrackMyWalks.ViewModels
{
    public class WalksTrailViewModel : WalkBaseViewModel
    {
        WalkEntries _walkEntry;
        public WalkEntries WalkEntry
        {
            get { return _walkEntry; }
            set {
                _walkEntry = value;
                OnPropertyChanged();
            }
        }

        public WalksTrailViewModel(WalkEntries walkEntry)
        {
            _walkEntry = walkEntry;
        }
    }
}
