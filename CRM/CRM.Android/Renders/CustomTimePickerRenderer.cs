using System;
using System.Runtime.Remoting.Contexts;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using CRM.CustomControls;
using CRM.Droid.Renders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomTimePicker), typeof(CustomTimePickerRenderer))]
namespace CRM.Droid.Renders
{
    class CustomTimePickerRenderer : TimePickerRenderer
    {
        CustomTimePicker element;
        public static void Init() { }

        public CustomTimePickerRenderer()
        {}

        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);
            //if (e.OldElement == null)
            //{
            //Control.Background = null;

            //var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
            //layoutParams.SetMargins(0, 0, 0, 0);
            //LayoutParameters = layoutParams;
            //GradientDrawable gd = new GradientDrawable();
            //gd.SetStroke(0, Android.Graphics.Color.LightGray);
            //Control.SetBackgroundDrawable(gd);
            //Control.LayoutParameters = layoutParams;
            //Control.SetPadding(0, 0, 0, 0);
            //SetPadding(0, 0, 0, 0);


            element = (CustomTimePicker)this.Element;

            if (Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))
                Control.Background = AddPickerStyles(element.Image);
            //}
        }

        public LayerDrawable AddPickerStyles(string imagePath)
        {
            //ShapeDrawable border = new ShapeDrawable();
            //border.Paint.Color = Android.Graphics.Color.White;
            //border.SetPadding(10, 10, 10, 10);
            //border.Paint.SetStyle(Paint.Style.Stroke);

            //Drawable[] layers = { border, GetDrawable(imagePath) };
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