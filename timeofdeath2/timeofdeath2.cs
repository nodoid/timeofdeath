using System;

using Xamarin.Forms;
using System.Linq;
using System.ComponentModel;

namespace timeofdeath2
{

    public class CommonVariables : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public int PartClothed
        { get; set; }

        public int Air
        { get; set; }

        public int Water
        { get; set; }

        public int PartialAir
        { get; set; }

        public double BodyWeight
        { get; set; }

        public double BodyTemperature
        { get; set; }

        public double SurroundTemperature
        { get; set; }

        public DateTime DateOfDeath
        { get; set; }

        public DateTime SetDate
        { get; set; }

        public string GetDate
        {
            get
            {
                var split = DateOfDeath.ToString().Split(' ').ToArray();
                OnPropertyChanged("CommonVariables");
                return split.Length == 0 ? string.Empty : split[0];
            }
        }

        public string GetTime
        {
            get
            {
                var split = DateOfDeath.ToString().Split(' ').ToArray();
                OnPropertyChanged("CommonVariables");
                return split.Length == 0 ? string.Empty : string.Format("{0}{1}", split[1], split.Length > 2 ? (!string.IsNullOrEmpty(split[2]) ? split[2].ToLower() : "") : "");
            }
        }

        public bool CalcDone { get; set; }
    }

   
    public class App : Application
    {
        public static Size ScreenSize { get; set; }

        public static App Self { get; private set; }

        public CommonVariables commonVariables { get; set; }

        public App()
        {
            // We need to get the screensize from the phone
            // we create the ScreenSize variable which can be accessed from either the PCL or the platform
            Self = this;

            commonVariables = new CommonVariables()
            {
                BodyTemperature = -999,
                SurroundTemperature = -999,
                Air = 0,
                PartialAir = 0,
                PartClothed = 0,
                Water = 0,
                SetDate = DateTime.Now,
                DateOfDeath = DateTime.Now,
                CalcDone = false
            };

            // launch the app
            MainPage = new TabPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

