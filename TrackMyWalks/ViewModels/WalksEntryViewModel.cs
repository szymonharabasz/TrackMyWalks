using TrackMyWalks.Models;
using TrackMyWalks.ViewModels;
using Xamarin.Forms;

namespace TrackMyWalks.ViewModels
{
    public class WalksEntryViewModel : WalkBaseViewModel
    {
        public WalksEntryViewModel()
        {
            Title = "Nowy szlak";
            Difficulty = "Niski";
            Distace = 1.0;
        }

        string _title;
        public string Title
        {
            get { return _title; }
            set {
                _title = value;
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }
        string _notes;
        public string Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                OnPropertyChanged();
            }
        }
        double _latitude;
        public double Latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                OnPropertyChanged();
            }
        }
        double _longitude;
        public double Longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                OnPropertyChanged();
            }
        }
        double _kilometers;
        public double Kilometers
        {
            get { return _kilometers; }
            set
            {
                _kilometers = value;
                OnPropertyChanged();
            }
        }
        string _difficulty;
        public string Difficulty
        {
            get { return _difficulty; }
            set
            {
                _difficulty = value;
                OnPropertyChanged();
            }
        }
        double _distance;
        public double Distace
        {
            get { return _distance; }
            set
            {
                _distance = value;
                OnPropertyChanged();
            }
        }
        string _imageUrl;
        public string ImageUrl
        {
            get { return ImageUrl; }
            set
            {
                _imageUrl = value;
                OnPropertyChanged();
            }
        }

        Command _saveCommand;
        public Command SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new Command(ExecuteSaveCommand, ValidateFormDetails));
            }
        }

        void ExecuteSaveCommand()
        {
            var newWalkItem = new WalkEntries
            {
                Title = this.Title,
                Notes = this.Notes,
                Latitude = this.Latitude,
                Longitude = this.Longitude,
                Kilometers = this.Kilometers,
                Difficulty = this.Difficulty,
                Distance = this.Distace,
                ImageUrl = this.ImageUrl
            };
        }
        bool ValidateFormDetails()
        {
            return !string.IsNullOrWhiteSpace(Title);
        }

    }
}
