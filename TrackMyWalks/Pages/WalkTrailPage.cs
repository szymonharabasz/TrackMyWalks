using System;

using Xamarin.Forms;
using TrackMyWalks.Models;
using TrackMyWalks.ViewModels;

namespace TrackMyWalks.Pages
{
    public class WalkTrailPage : ContentPage
    {
        public WalkTrailPage(WalkEntries walkItem)
        {
            Title = "Szlak";

            BindingContext = new WalksTrailViewModel(walkItem);

            var beginTrailWalk = new Button
            {
                Background = Color.FromHex("#008080"),
                TextColor = Color.White,
                Text = "Rozpocznij szlak"
            };

            beginTrailWalk.Clicked += (sender, e) =>
            {
                if (walkItem == null) return;
                Navigation.PushAsync(new DistanceTravelledPage(walkItem));
                Navigation.RemovePage(this);
                walkItem = null;
            };

            var walkTrailImage = new Image()
            {
                Aspect = Aspect.Fill,
            };
            walkTrailImage.SetBinding(Image.SourceProperty, "WalkEntry.ImageUrl");

            var trailNameLabel = new Label()
            {
                FontSize = 28,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
            };
            trailNameLabel.SetBinding(Label.TextProperty, "WalkEntry.Title");

            var trailKilemetersLabel = new Label()
            {

                FontSize = 12,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
            };
            trailKilemetersLabel.SetBinding(Label.TextProperty, "WalkEntry.Kilometers", stringFormat: "Długość: {0} km");

            var trailDifficultyLabel = new Label()
            {

                FontSize = 12,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
            };
            trailDifficultyLabel.SetBinding(Label.TextProperty, "WalkEntry.Difficulty", stringFormat: "Poziom trudności: {0}");

            var trailFullDescription = new Label()
            {
                FontSize = 11,
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            trailFullDescription.SetBinding(Label.TextProperty, "WalkEntry.Notes");

            Content = new ScrollView
            {
                Padding = 10,
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    Children =
                    {
                        walkTrailImage,
                        trailNameLabel,
                        trailKilemetersLabel,
                        trailDifficultyLabel,
                        trailFullDescription,
                        beginTrailWalk
                    }
                }
            };
        }
    }
}

