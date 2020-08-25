using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Graphics.Drawable;
using Android.Views;
using Android.Widget;
using CRM.CustomControls;
using CRM.Droid.Renders;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(NavigationPageGradientHeader), typeof(NavigationPageGradientHeaderRenderer))]
namespace CRM.Droid.Renders
{
    [Obsolete]
    public class NavigationPageGradientHeaderRenderer : NavigationPageRenderer
    {
        //private static Android.Support.V7.Widget.Toolbar GetToolbar() => (CrossCurrentActivity.Current?.Activity as MainActivity)?.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            try
            {
                base.OnElementChanged(e);

                //run once when element is created
                if (e.OldElement != null || Element == null)
                    return;

                //Android.Support.V7.Widget.Toolbar toolbar = this.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

                //var toolbar = GetToolbar();
                var control = (NavigationPageGradientHeader)this.Element;
                var context = ((MainActivity)this.Context);

                context.SupportActionBar.SetBackgroundDrawable(new GradientDrawable(GradientDrawable.Orientation.RightLeft, new int[] { control.RightColor.ToAndroid(), control.LeftColor.ToAndroid() }));

                //context.ActionBar.SetBackgroundDrawable(new GradientDrawable(GradientDrawable.Orientation.RightLeft, new int[] { control.RightColor.ToAndroid(), control.LeftColor.ToAndroid() }));
                //context.ActionBar.SetBackgroundDrawable(new GradientDrawable(GradientDrawable.Orientation.RightLeft, new int[] { control.RightColor.ToAndroid(), control.LeftColor.ToAndroid() }));
            }
            catch (Exception ex)
            {
            }
        }
    }
}