using System;
using System.Threading.Tasks;
using TrackMyWalks.ViewModels;

namespace TrackMyWalks.Services
{
    public interface IWalkNavService
    {
        Task PreviousPage();
        Task BackToMainPage();
        Task NavigateToViewModel<ViewModel, TParameter>(TParameter parameter)
            where ViewModel : WalkBaseViewModel;
    }
}
