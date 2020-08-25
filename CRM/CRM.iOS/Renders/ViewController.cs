using System;
using Foundation;
using VideoSplash;
using Xamarin.Forms;

namespace CRM.iOS.Renders
{
    partial class ViewController : VideoViewController
    {
        public ViewController()
        {
        }

        public ViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override bool PrefersStatusBarHidden()
        {
            return true;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            StartTime = 0f;
            Duration = 2.9f;
            VideoUrl = new NSUrl(NSBundle.MainBundle.PathForResource("splash", "mp4"), false);

            NSTimer.CreateScheduledTimer(3, false, (obj) =>
            {
                MessagingCenter.Send<object, object>(this, "ShowMainScreen", null);
            });

        }
    }
}