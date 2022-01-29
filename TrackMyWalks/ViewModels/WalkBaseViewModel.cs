using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TrackMyWalks.ViewModels
{
    public class WalkBaseViewModel : INotifyPropertyChanged
    {
        public WalkBaseViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
