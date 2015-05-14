using Xamarin.Forms;
using System.Linq;

namespace timeofdeath2
{
    public class Results : ContentPage
    {
        public Results()
        {
            if (Device.OS == TargetPlatform.iOS)
                Padding = new Thickness(0, 20, 0, 0);

            BindingContext = App.Self.commonVariables;

            var lblTime = new Label(){ WidthRequest = App.ScreenSize.Width / 4, };
            var lblDate = new Label(){ WidthRequest = App.ScreenSize.Width / 4,  };
            lblDate.SetBinding(Label.TextProperty, "GetDate");
            lblTime.SetBinding(Label.TextProperty, "GetTime");

            Content = new StackLayout
            { 
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(20),
                Children =
                {
                    new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new Label(){ Text = "Death occured on or before ", WidthRequest = (App.ScreenSize.Width / 4) * 3 },
                            lblTime
                        }
                    },
                    new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new Label(){ Text = "Date of death ", WidthRequest = (App.ScreenSize.Width / 4) * 3 },
                            lblDate
                        }
                    }
                }
            };
        }
    }
}


