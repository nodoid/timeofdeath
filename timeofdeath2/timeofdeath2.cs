using System;

using Xamarin.Forms;
using System.Linq;

namespace timeofdeath2
{

    public static class CommonVariables
    {
        public static int PartClothed
        { get; set; }

        public static int Air
        { get; set; }

        public static int Water
        { get; set; }

        public static int PartialAir
        { get; set; }

        public static double BodyWeight
        { get; set; }

        public static double BodyTemperature
        { get; set; }

        public static double SurroundTemperature
        { get; set; }

        public static DateTime DateOfDeath
        { get; set; }

        public static DateTime SetDate
        { get; set; }

        public static string GetDate
        {
            get
            {
                var split = CommonVariables.DateOfDeath.ToString().Split(' ').ToArray();
                return split.Length == 0 ? string.Empty : split[0];
            }
        }

        public static string GetTime
        {
            get
            {
                var split = CommonVariables.DateOfDeath.ToString().Split(' ').ToArray();
                return split.Length == 0 ? string.Empty : string.Format("{0}{1}", split[1], split.Length > 2 ? (!string.IsNullOrEmpty(split[2]) ? split[2].ToLower() : "") : "");
            }
        }

        public static bool CalcDone { get; set; }
    }

   
    public class App : Application
    {
        public static Size ScreenSize { get; set; }

        public App()
        {
            // We need to get the screensize from the phone
            // we create the ScreenSize variable which can be accessed from either the PCL or the platform

            // Set up some variables
            CommonVariables.BodyTemperature = CommonVariables.SurroundTemperature = -999;
            CommonVariables.Air = CommonVariables.PartialAir = CommonVariables.PartClothed = CommonVariables.Water = 0;
            CommonVariables.SetDate = CommonVariables.DateOfDeath = DateTime.Now;
            CommonVariables.CalcDone = false;

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

