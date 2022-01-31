using System;

using Xamarin.Forms;
using TrackMyWalks.Models;
using TrackMyWalks.ViewModels;
using TrackMyWalks.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrackMyWalks.Pages
{
    public class WalksPage : ContentPage
    {
        WalksPageViewModel _viewModel
        {
            get
            {
                return BindingContext as WalksPageViewModel;
            }
        }

        public WalksPage()
        {

            var newWalkItem = new ToolbarItem
            {
                Text = "Dodaj szlak"
            };
            newWalkItem.SetBinding(ToolbarItem.CommandProperty, "CreatenewWalk");
            ToolbarItems.Add(newWalkItem);

            BindingContext = new WalksPageViewModel(DependencyService.Get<IWalkNavService>());



            var itemTemplate = new DataTemplate(typeof(ImageCell));
            itemTemplate.SetBinding(TextCell.TextProperty, "Title");
            itemTemplate.SetBinding(TextCell.DetailProperty, "Notes");
            itemTemplate.SetBinding(ImageCell.ImageSourceProperty, "ImageUrl");

            var walksList = new ListView
            {
                HasUnevenRows = true,
                ItemTemplate = itemTemplate,
                SeparatorColor = Color.FromHex("#ddd")
            };
            walksList.SetBinding(ItemsView<Cell>.ItemsSourceProperty, "walkEntries");
            walksList.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                var item = (WalkEntries)e.Item;
                if (item == null) return;
                _viewModel.WalkTrailDetails.Execute(item);
                item = null;
            };

            Content = walksList;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (_viewModel != null)
                await _viewModel.Init();
        }
    }
}

