using System;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using CRM.CustomControls;
using CRM.Droid.Renders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace CRM.Droid.Renders
{
    [Obsolete]
    public class CustomDatePickerRenderer : DatePickerRenderer
    {
        CustomDatePicker element;
        public static void Init() { }
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            element = (CustomDatePicker)this.Element;

            if (Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))
                Control.Background = AddPickerStyles(element.Image);
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
                        height = width = 38;
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