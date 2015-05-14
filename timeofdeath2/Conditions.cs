using System;

using Xamarin.Forms;
using System.Collections.Generic;

namespace timeofdeath2
{
    public class Conditions : ContentPage
    {
        // we need the pickers to be globally accessible within this class
        Picker pickBodyCond, pickAirCond, pickWaterCond, pickWaterMove;

        public Conditions()
        {
            if (Device.OS == TargetPlatform.iOS)
                Padding = new Thickness(0, 20, 0, 0);
            CreateUI();
        }

        void CreateUI()
        {
            // set up the lists for the spinners

            var bodyConditions = new List<string>
            {
                "Dry and naked",
                "Dry with 1-2 thin layers",
                "Dry with 2-3 thin layers",
                "Dry with 3-4 thin layers",
                "Dry with 1-2 thicker layers",
                "Dry with more thin or thicker layers",
                "Wet and naked",
                "Wet with 1-2 thin wet layers",
                "Wet with 2 thicker wet layers",
                "Wet with 2 or more thicker wet layers"
            };

            var airConditions = new List<string>
            {
                "still",
                "moving",
                "unknown"
            };

            var waterCondition = new List<string>
            {
                "moving",
                "still",
                "pulled from the water",
                "unknown"
            };

            var pulledFromWater = new List<string>
            {
                "still",
                "moving"
            };

            // create the pickers
            pickBodyCond = new Picker()
            {
                SelectedIndex = 0,
                StyleId = "pickBodyCond",
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            pickAirCond = new Picker()
            {
                SelectedIndex = 0,
                StyleId = "pickAirCond",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                IsEnabled = false
            };

            pickWaterCond = new Picker()
            {
                SelectedIndex = 0,
                StyleId = "pickWaterCond",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                IsEnabled = false,
            };

            pickWaterMove = new Picker()
            {
                SelectedIndex = 0,
                StyleId = "pickWaterMove",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                IsEnabled = false
            };

            // add the event
            pickAirCond.SelectedIndexChanged += SpinnerChanged;
            pickBodyCond.SelectedIndexChanged += SpinnerChanged;
            pickWaterCond.SelectedIndexChanged += SpinnerChanged;
            pickWaterMove.SelectedIndexChanged += SpinnerChanged;

            // the picker (as supplied by Xamarin) dropdowns are not bindable to (at the time of writing) for the drop down.
            // see chapter 11 on using Xamarin.Forms.Labs for the picker extension

            foreach (var bc in bodyConditions)
                pickBodyCond.Items.Add(bc);

            foreach (var ac in airConditions)
                pickAirCond.Items.Add(ac);

            foreach (var wc in waterCondition)
                pickWaterCond.Items.Add(wc);

            foreach (var pw in pulledFromWater)
                pickWaterMove.Items.Add(pw);

            var btnCalculate = new Button()
            {
                Text = "Calculate time of death",
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            btnCalculate.Clicked += CalculateToD;

            // create the stack

            Content = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(20),
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    new Label() { Text = "When found, the body was" },
                    pickBodyCond,
                    new Label() { Text = "If found in air, the air was" },
                    pickAirCond,
                    new Label() { Text = "If found in water, it was" },
                    pickWaterCond,
                    new Label() { Text = "If pulled from water, the air was" },
                    pickWaterMove,
                    new StackLayout()
                    {
                        Padding = new Thickness(0, 10),
                        Children =
                        {
                            btnCalculate
                        }
                    }
                }
            };
        }

        void SpinnerChanged(object s, EventArgs e)
        {
            var spin = s as Picker;
            switch (spin.StyleId)
            {
                case "pickBodyCond":
                    if (spin.SelectedIndex >= 6)
                    {
                        CommonVariables.Water = 1;
                        CommonVariables.Air = -2;
                        pickWaterCond.IsEnabled = true;
                        pickAirCond.IsEnabled = false;
                    }
                    else
                    {
                        CommonVariables.Water = 0;
                        pickWaterCond.IsEnabled = false;
                        pickAirCond.IsEnabled = true;
                    }
                    CommonVariables.PartialAir = spin.SelectedIndex;
                    break;
                case "pickAirCond":
                    CommonVariables.Air = spin.SelectedIndex;
                    CommonVariables.PartialAir = CommonVariables.Water = -1;
                    break;
                case "pickWaterCond":
                    if (spin.SelectedIndex == 2)
                    {
                        pickWaterMove.IsEnabled = true;
                        CommonVariables.PartialAir = 0;
                    }
                    else
                        pickWaterMove.IsEnabled = false;
                    CommonVariables.Air = -2;
                    CommonVariables.Water = spin.SelectedIndex;
                    break;
                case "pickWaterMove":
                    CommonVariables.Air = -2;
                    CommonVariables.PartialAir = spin.SelectedIndex;
                    break;
            }
        }

        async void CalculateToD(object s, EventArgs e)
        {
            // we need to put in some traps before we can calculate

            if (CommonVariables.SurroundTemperature == -999)
            {
                await DisplayAlert("Calculator error", "Temperature of surrounds is null", "OK");
                return;
            }

            if (CommonVariables.BodyTemperature == -999)
            {
                await DisplayAlert("Calculator error", "Temperature of the body is null", "OK");
                return;
            }

            if (CommonVariables.BodyTemperature < CommonVariables.SurroundTemperature)
            {
                await DisplayAlert("Calculator error", "The body temperature has to be higher than the surrounds", "OK");
                return;
            }

            if (CommonVariables.BodyWeight <= 0)
            {
                await DisplayAlert("Calculator error", "The body has to have some weight", "OK");
                return;
            }

            var calc = new TODCalc();
            calc.CalcTOD(CommonVariables.DateOfDeath);

            CommonVariables.CalcDone = true;

            var masterPage = Parent as TabbedPage;
            masterPage.CurrentPage = masterPage.Children[2];
        }
    }
}


