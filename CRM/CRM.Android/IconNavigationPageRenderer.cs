using Android.Content;
using Android.Support.V7.Graphics.Drawable;
using CRM.Droid;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(CRM.View.Menu.MasterDetailView), typeof(IconNavigationPageRenderer))]
namespace CRM.Droid
{
    public class IconNavigationPageRenderer : MasterDetailPageRenderer
    {
        private static Android.Support.V7.Widget.Toolbar GetToolbar() => (CrossCurrentActivity.Current?.Activity as MainActivity)?.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

        public IconNavigationPageRenderer(Context context) : base(context)
        {

        }
        
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            var toolbar = GetToolbar();
            //var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            if (toolbar != null)
            {
                for (var i = 0; i < toolbar.ChildCount; i++)
                {
                    var imageButton = toolbar.GetChildAt(i) as Android.Widget.ImageButton;
                    var drawerArrow = imageButton?.Drawable as DrawerArrowDrawable;
                    if (drawerArrow == null)
                        continue;

                    //imageButton.SetImageDrawable(Context.GetDrawable(Resource.Drawable.ic_menu));

                    bool displayBack = false;
                    var app = Xamarin.Forms.Application.Current;
                    if (app.MainPage is MasterDetailPage)
                    {
                        var detailPage = (app.MainPage as MasterDetailPage).Detail;

                        var navPageLevel = detailPage.Navigation.NavigationStack.Count;
                        if (navPageLevel > 1)
                            displayBack = true;

                        //if (!displayBack)
                        //    ChangeIcon(imageButton, Resource.Drawable.ic_menu);
                        //if (displayBack)
                         //   ChangeIcon(imageButton, Resource.Drawable.ic_back1);
                    }
                }
            }
        }
        private void ChangeIcon(Android.Widget.ImageButton imageButton, int id)
        {
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
                imageButton.SetImageDrawable(Context.GetDrawable(id));
            imageButton.SetImageResource(id);
        }
    }
}