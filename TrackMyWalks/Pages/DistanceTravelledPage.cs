﻿using System;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using TrackMyWalks.Models;
using TrackMyWalks.ViewModels;
using TrackMyWalks.Services;

namespace TrackMyWalks.Pages
{
    public class DistanceTravelledPage : ContentPage
    {
        DistanceTravelledViewModel _viewModel
        {
            get { return BindingContext as DistanceTravelledViewModel; }
        }

        public DistanceTravelledPage()
        {
            Title = "Przebyty dystans";

            BindingContext = new DistanceTravelledViewModel(DependencyService.Get<IWalkNavService>());

            var trailMap = new Map();
            trailMap.Pins.Add(new Pin
            {
                Type = PinType.Place,
                Label = _viewModel.WalkEntry.Title,
                Position = new Position(
                    _viewModel.WalkEntry.Latitude,
                    _viewModel.WalkEntry.Longitude   
            )});
            trailMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(
                _viewModel.WalkEntry.Latitude,
                _viewModel.WalkEntry.Longitude
            ), Distance.FromKilometers(1.0)));

            var trailNameLabel = new Label()
            {
                FontSize = 18,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
            };
            trailNameLabel.SetBinding(Label.TextProperty, "WalkEntry.Title");

            var trailDistanceTravelledLabel = new Label()
            {
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
                HorizontalTextAlignment = TextAlignment.Center
            };
            trailDistanceTravelledLabel.SetBinding(Label.TextProperty, "Travelled",
                stringFormat: "Przebyty dystans {0} km");

            var totalDistanceTaken = new Label()
            {

                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
                Text = $"{_viewModel.WalkEntry.Distance} km",
                HorizontalTextAlignment = TextAlignment.Center
            };

            var totalTimeTakenLabel = new Label()
            {
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
                HorizontalTextAlignment = TextAlignment.Center
            };
            totalTimeTakenLabel.SetBinding(Label.TextProperty, "TimeTaken",
                stringFormat: "Czas: {0}");

            var totalTimeTaken = new Label()
            {

                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
                Text = "0 h 0 m 0 s",
                HorizontalTextAlignment = TextAlignment.Center

            };

            var walksHomeButton = new Button
            {
                BackgroundColor = Color.FromHex("#008080"),
                TextColor = Color.White,
                Text = "Zakończ ten szlak"
            };
            walksHomeButton.Clicked += (sender, e) =>
            {
                if (_viewModel.WalkEntry == null) return;
                _viewModel.BackToMainPage.Execute(0);
            };

            this.Content = new ScrollView
            {
                Padding = 10,

                Content = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,

                    Children = {
                        trailMap,
                        trailNameLabel,
                        trailDistanceTravelledLabel,
                        totalDistanceTaken,
                        totalTimeTakenLabel,
                        totalTimeTaken,
                        walksHomeButton
                    }
                }
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (_viewModel != null)
            {
                await _viewModel.Init();
            }
        }
    }
}

