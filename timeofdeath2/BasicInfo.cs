using System;

using Xamarin.Forms;

namespace timeofdeath2
{
    public class BasicInfo : ContentPage
    {
        public BasicInfo()
        {
            if (Device.OS == TargetPlatform.iOS)
                Padding = new Thickness(0, 20, 0, 0);

            CreateUI();
        }

        void CreateUI()
        {
            var datePicker = new DatePicker()
            {
                Date = DateTime.Now,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            var timePicker = new TimePicker()
            {
                Time = DateTime.Now.TimeOfDay,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            // the original version had some UI issues, so let's give the the edit boxes and spinner some size
            // the spinner will be split 50%, 25%, 25%
            // rest are 75% label, 25% entry

            var twentyFivePC = App.ScreenSize.Width / 4;

            var editBodyTemp = new Entry()
            {
                Keyboard = Keyboard.Numeric,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = twentyFivePC
            };
            var editTempSurrounds = new Entry()
            {
                Keyboard = Keyboard.Numeric,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = twentyFivePC
            };
            var editWeight = new Entry()
            {
                Keyboard = Keyboard.Numeric,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = twentyFivePC
            };
            var pickWeightUnits = new Picker()
            {
                WidthRequest = twentyFivePC,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            pickWeightUnits.Items.Add("in Kg");
            pickWeightUnits.Items.Add("in lbs");

            if (Device.OS == TargetPlatform.iOS)
                editWeight.HeightRequest = editTempSurrounds.Height;

            editBodyTemp.TextChanged += (object sender, TextChangedEventArgs e) => CommonVariables.BodyTemperature = !string.IsNullOrEmpty(editBodyTemp.Text) ? Convert.ToDouble(editBodyTemp.Text) : 0;
            editTempSurrounds.TextChanged += (object sender, TextChangedEventArgs e) => CommonVariables.SurroundTemperature = !string.IsNullOrEmpty(editTempSurrounds.Text) ? Convert.ToDouble(editTempSurrounds.Text) : 0;
            editWeight.TextChanged += (object sender, TextChangedEventArgs e) =>
            {
                var weight = !string.IsNullOrEmpty(editWeight.Text) ? Convert.ToDouble(editWeight.Text) : 0;
                weight *= pickWeightUnits.SelectedIndex == 0 ? 6.35029318 : 1;
                CommonVariables.BodyWeight = weight;
            };

            datePicker.DateSelected += (object sender, DateChangedEventArgs e) =>
            {
                CommonVariables.DateOfDeath = e.NewDate;
            };

            timePicker.PropertyChanged += (sender, e) =>
            {
                var date = CommonVariables.DateOfDeath;
                var time = timePicker.Time;
                CommonVariables.DateOfDeath = new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds);
            };

            Content = new StackLayout
            { 
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(20),
                Children =
                {
                    new Label { Text = "Enter the date the body was found", WidthRequest = twentyFivePC * 3 },
                    datePicker,
                    new Label(){ Text = "Time the body was found", WidthRequest = twentyFivePC * 3 },
                    timePicker, 
                    new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Padding = new Thickness(0, 20),
                        Children =
                        {
                            new Label(){ Text = "What temperature was the body when found (deg C)", WidthRequest = twentyFivePC * 3 },
                            editBodyTemp
                        }
                    },
                    new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Padding = new Thickness(0, 20),
                        Children =
                        {
                            new Label(){ Text = "What is the temperature of the surrounds (deg C)", WidthRequest = twentyFivePC * 3 },
                            editTempSurrounds
                        }
                    },
                    new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Padding = new Thickness(0, 20),
                        Children =
                        {
                            new Label(){ Text = "What is the weight of the body", WidthRequest = twentyFivePC * 2 },
                            editWeight,
                            pickWeightUnits
                        }
                    }
                }
            };
        }
    }
}


