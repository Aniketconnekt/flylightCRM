using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using CRM.CustomControls;
using CRM.Droid.Renders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(MasterDetailPage), typeof(CustomMasterDetailRenderer))]
namespace CRM.Droid.Renders
{
	[Obsolete]
    public class CustomMasterDetailRenderer : MasterDetailPageRenderer
    {
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            for (int index = 0; index < toolbar.ChildCount; index++)
            {
                if (toolbar.GetChildAt(index) is TextView)
                {
                    var title = toolbar.GetChildAt(index) as TextView;
                    float toolbarCenter = toolbar.MeasuredWidth / 2;
                    float titleCenter = title.MeasuredWidth / 2;
                    title.SetX(toolbarCenter - titleCenter);
                }
            }
        }
    }
}