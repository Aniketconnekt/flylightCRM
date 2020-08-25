using System;
using System.Collections.Generic;
using System.Linq;
using CRM.iOS.Renders;
using Foundation;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.SfPicker.XForms.iOS;
using UIKit;
using Xamarin.Forms;

namespace CRM.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();            
            SfListViewRenderer.Init();
            SfPickerRenderer.Init();

            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            ViewController control = new ViewController();

            Window.RootViewController = control;
            Window.MakeKeyAndVisible();

            MessagingCenter.Subscribe<object, object>(this, "ShowMainScreen", (sender, args) =>
            {
                LoadApplication(new App());
                base.FinishedLaunching(app, options);
            });


            return true;
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            if (Xamarin.Essentials.Platform.OpenUrl(app, url, options))
                return true;

            return base.OpenUrl(app, url, options);
        }
    }
}
