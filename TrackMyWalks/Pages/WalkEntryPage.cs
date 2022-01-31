using System;

using Xamarin.Forms;
using TrackMyWalks.Models;
using TrackMyWalks.ViewModels;
using TrackMyWalks.Services;

namespace TrackMyWalks.Pages
{
    public class WalkEntryPage : ContentPage
    {
        WalksEntryViewModel _viewModwl
        {
            get
            {
                return BindingContext as WalksEntryViewModel;
            }
        }

        public WalkEntryPage()
        {
            Title = "Nowy wpis";

            BindingContext = new WalksEntryViewModel(DependencyService.Get<IWalkNavService>());

            var walkTitle = new EntryCell
            {
                Label = "Tytuł:",
                Placeholder = "Nazwa szlaku"
            };
            walkTitle.SetBinding(EntryCell.TextProperty, "Title", BindingMode.TwoWay);

            var walkNotes = new EntryCell
            {
                Label = "Uwagi:",
                Placeholder = "Opis"
            };
            walkNotes.SetBinding(EntryCell.TextProperty, "Notes", BindingMode.TwoWay);

            var walkLatitude = new EntryCell
            {
                Label = "Szerokość geograficzna",
                Placeholder = "Szerokość",
                Keyboard = Keyboard.Numeric
            };
            walkLatitude.SetBinding(EntryCell.TextProperty, "Latitude", BindingMode.TwoWay);

            var walkLongitude = new EntryCell
            {
                Label = "Długość geograficzna",
                Placeholder = "",
                Keyboard = Keyboard.Numeric
            };
            walkLongitude.SetBinding(EntryCell.TextProperty, "Longitude", BindingMode.TwoWay);

            var walkKilometers = new EntryCell
            {
                Label = "Liczba kilometrów",
                Placeholder = "Liczba kolemetrów",
                Keyboard = Keyboard.Numeric
            };
            walkKilometers.SetBinding(EntryCell.TextProperty, "Kilometers", BindingMode.TwoWay);

            var walkDifficulty = new EntryCell
            {
                Label = "Poziom trudności",
                Placeholder = "Poziom trudności szlaku"
            };
            walkDifficulty.SetBinding(EntryCell.TextProperty, "Difficulty", BindingMode.TwoWay);

            var walkImageUrl = new EntryCell
            {
                Label = "URL obrazu",
                Placeholder = "URL obrazu"
            };
            walkImageUrl.SetBinding(EntryCell.TextProperty, "ImageUrl", BindingMode.TwoWay);

            Content = new TableView
            {
                Intent = TableIntent.Form,
                Root = new TableRoot
                {
                    new TableSection
                    {
                        walkTitle,
                        walkNotes,
                        walkLatitude,
                        walkLongitude,
                        walkKilometers,
                        walkDifficulty,
                        walkImageUrl
                    }
                }
            };

            var saveWalkItem = new ToolbarItem
            {
                Text = "Zapisz"
            };
            saveWalkItem.SetBinding(MenuItem.CommandProperty, "SaveCommand");
            saveWalkItem.Clicked += (sender, e) =>
            {
                Navigation.PopToRootAsync(true);
            };
            ToolbarItems.Add(saveWalkItem);
        }
    }
}

