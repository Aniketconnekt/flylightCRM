using System;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using CRM.Common.Constants;
using CRM.CustomControls;
using CRM.Droid.Renders;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace CRM.Droid.Renders
{
    [Obsolete]
    public class CustomPickerRenderer : PickerRenderer
    {
        CustomPicker element;

        protected async override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            element = (CustomPicker)this.Element;

            if (Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))
            {
                var userId = CRM.Common.Helpers.Settings.CRM_UserRole; //await SecureStorage.GetAsync(AppConstant.UserRole);
                //if (userId == "2")
                //    element.Image = "ic_dropdown_green";
                //else
                    element.Image = "ic_dropdown";
                //element.TitleColor = Xamarin.Forms.Color.FromHex(App.nav_bar_color);
                //element.TextColor = Xamarin.Forms.Color.FromHex(App.nav_bar_color);
                Control.Background = AddPickerStyles(element.Image);
            }   
        }

        public LayerDrawable AddPickerStyles(string imagePath)
        {
            Drawable[] layers = { GetDrawable(imagePath) };
            LayerDrawable layerDrawable = new LayerDrawable(layers);
            layerDrawable.SetLayerInset(0, 0, 0, 0, 0);

            return layerDrawable;
        }

        private BitmapDrawable GetDrawable(string imagePath)
        {
            try
            {
                int width = 0;
                int height = 0;

                switch (Device.Idiom)
                {
                    case TargetIdiom.Phone:
                        height = width = 20;
                        break;
                    case TargetIdiom.Tablet:
                        height = width = 40;
                        break;
                }
                
                int resID = Resources.GetIdentifier(imagePath, "drawable", this.Context.PackageName);
                var drawable = ContextCompat.GetDrawable(this.Context, resID);
                var bitmap = ((BitmapDrawable)drawable).Bitmap;

                var result = new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, width, height, true));
                result.Gravity = Android.Views.GravityFlags.Right;

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
