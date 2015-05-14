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

            // we need to modify the time of death and date of death as we don't have the ToShortDateString and ToShortTimeString within the PCL
            // we could always just put an interface to the platform, but it's hardly worth the effort

            string ToD = "", DoD = "";

            var split = CommonVariables.DateOfDeath.ToString().Split(' ').ToArray();

            // when the DateTime is split, it's of the form split[0] = date, split[1] = time (12hr), split[2] = AM/PM
            // split[2] may or may not exist

            BindingContext = "CommonVariables";

            var lblTime = new Label(){ WidthRequest = App.ScreenSize.Width / 4 };
            var lblDate = new Label(){ WidthRequest = App.ScreenSize.Width / 4 };
            lblDate.SetBinding(Label.TextProperty, "GetDate");
            lblTime.SetBinding(Label.TextProperty, "GetTime");
            this.BindingContextChanged += (object sender, System.EventArgs e) =>
            {
                lblDate.SetBinding(Label.TextProperty, "GetDate");
                lblTime.SetBinding(Label.TextProperty, "GetTime");
            };


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


