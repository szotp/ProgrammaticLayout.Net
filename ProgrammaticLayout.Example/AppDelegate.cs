using Foundation;
using UIKit;

namespace ProgrammaticLayout.Example
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations

        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            Window = new UIWindow();
            Window.RootViewController = new UINavigationController(new ViewController());
            Window.MakeKeyAndVisible();
            return true;
        }

    }
}

