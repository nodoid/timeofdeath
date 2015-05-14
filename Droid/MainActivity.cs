
using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms;

namespace timeofdeath2.Droid
{
    [Activity(Label = "timeofdeath2.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            App.ScreenSize = new Size(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density, Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density);

            LoadApplication(new App());
        }
    }
}

