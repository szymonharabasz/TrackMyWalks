using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TrackMyWalks.Services;
using TrackMyWalks.ViewModels;
using Xamarin.Forms;

[assembly: Dependency(typeof(WalkNavService))]
namespace TrackMyWalks.Services
{
    public class WalkNavService : IWalkNavService
    {
        public WalkNavService()
        {
        }

        public INavigation navigation { get; set; }
        readonly IDictionary<Type, Type> _viewMapping = new Dictionary<Type, Type>();

        public void RegisterViewMaping(Type viewModel, Type view)
        {
            _viewMapping.Add(viewModel, view);
        }

        public async Task PreviousPage()
        {
            if (navigation.NavigationStack != null && navigation.NavigationStack.Count > 0)
            {
                await navigation.PopAsync(true);
            }
        }

        public async Task BackToMainPage()
        {
            await navigation.PopToRootAsync(); 
        }

        public async Task NavigateToViewModel<ViewModel, TParameter>(TParameter parameter) where ViewModel : WalkBaseViewModel
        {
            Type viewType;

            if (_viewMapping.TryGetValue(typeof(ViewModel), out viewType))
            {
                var constructor = viewType.GetTypeInfo().DeclaredConstructors
                    .FirstOrDefault(dc => dc.GetParameters().Count() <= 0);

                var view = constructor.Invoke(null) as Page;

                await navigation.PushAsync(view, true);
            }

            if (navigation.NavigationStack.Last().BindingContext is WalkBaseViewModel<WalkParam>)
            {
                await((WalkBaseViewModel<WalkParam>)(navigation.NavigationStack.Last().BindingContext))
                    .Init(parameter);
            }
        }
    }
}
