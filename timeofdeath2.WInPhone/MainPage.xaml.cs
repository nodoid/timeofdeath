using System;
using System.Windows;
using Microsoft.Phone.Controls;

namespace timeofdeath2.WInPhone
{
    public partial class MainPage : global::Xamarin.Forms.Platform.WinPhone.FormsApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            SupportedOrientations = SupportedPageOrientation.Portrait;

            global::Xamarin.Forms.Forms.Init();

            var content = Application.Current.Host.Content;
            var scale = (double)content.ScaleFactor / 100;
            var height = (int)Math.Ceiling(content.ActualHeight * scale);
            var width = (int)Math.Ceiling(content.ActualWidth * scale);

            timeofdeath2.App.ScreenSize = new Xamarin.Forms.Size((double)width, (double)height);

            LoadApplication(new timeofdeath2.App());
        }
    }
}