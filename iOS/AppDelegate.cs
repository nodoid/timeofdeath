
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace timeofdeath2.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            // Code for starting up the Xamarin Test Cloud Agent
            #if ENABLE_TEST_CLOUD
            Xamarin.Calabash.Start();
            #endif

            App.ScreenSize = new Size(UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}

