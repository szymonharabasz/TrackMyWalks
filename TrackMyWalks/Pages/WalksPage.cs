using System;

using Xamarin.Forms;
using TrackMyWalks.Models;
using TrackMyWalks.ViewModels;
using System.Collections.Generic;

namespace TrackMyWalks.Pages
{
    public class WalksPage : ContentPage
    {
        public WalksPage()
        {

            var newWalkItem = new ToolbarItem
            {
                Text = "Dodaj szlak"
            };
            newWalkItem.Clicked += (sender, e) =>
            {
                Navigation.PushAsync(new WalkEntryPage());
            };

            ToolbarItems.Add(newWalkItem);

            BindingContext = new WalksPageViewModel();



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
                Navigation.PushAsync(new WalkTrailPage(item));
                item = null;
            };

            Content = walksList;
        }
    }
}

