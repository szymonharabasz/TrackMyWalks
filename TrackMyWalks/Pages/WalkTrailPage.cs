using System;

using Xamarin.Forms;
using TrackMyWalks.Models;
using TrackMyWalks.ViewModels;
using TrackMyWalks.Services;

namespace TrackMyWalks.Pages
{
    public class WalkTrailPage : ContentPage
    {
        WalksTrailViewModel _viewModel
        {
            get
            {
                return BindingContext as WalksTrailViewModel;
            }
        }

        public WalkTrailPage(WalkEntries walkItem)
        {
            Title = "Szlak";

            BindingContext = new WalksTrailViewModel(DependencyService.Get<IWalkNavService>());

            var beginTrailWalk = new Button
            {
                Background = Color.FromHex("#008080"),
                TextColor = Color.White,
                Text = "Rozpocznij szlak"
            };

            beginTrailWalk.Clicked += (sender, e) =>
            {
                if (_viewModel.WalkEntry == null) return;
                _viewModel.DistanceTravelled.Execute(_viewModel.WalkEntry);
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

