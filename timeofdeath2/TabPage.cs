using Xamarin.Forms;

namespace timeofdeath2
{
    public class TabPage : TabbedPage
    {
        readonly Page BasicInfo, Conditions, Results;

        public TabPage()
        {
            // this will have no effect on the main page
            // it needs to be put into the child pages
            if (Device.OS == TargetPlatform.iOS)
                Padding = new Thickness(0, 20, 0, 0);
            
            CreateUI();
        }

        void CreateUI()
        {
            // the following won't show
            Title = "Tab pages";

            // create the links to the pages
            // Rather than use the ItemSource, we shall create Children to the page.
            // Remember, a tabbedpage is a container with each page associated with it
            // being a child from it

            Children.Add(new BasicInfo() { Title = "Basic info", Icon = "cross.png" });
            Children.Add(new Conditions() { Title = "Conditions", Icon = "weather.png" });
            Children.Add(new Results() { Title = "Results", Icon = "clock.png" });

            // Current page is the tabbedpage property denoting which of the child pages you're on
            CurrentPage = Children[0];
        }

        public void SwitchToTab1()
        {
            CurrentPage = BasicInfo;
        }

        public void SwitchToTab2()
        {
            CurrentPage = Conditions;
        }

        public void SwitchToTab3()
        {
            CurrentPage = Results;
        }
    }
}


