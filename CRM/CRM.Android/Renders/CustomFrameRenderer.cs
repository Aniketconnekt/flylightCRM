using Android.Content;
using CRM.Droid.Renders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

//[assembly: ExportRenderer(typeof(Frame),typeof(CustomFrameRenderer))]
namespace CRM.Droid.Renders
{
    public class CustomFrameRenderer : FrameRenderer
    {
        public CustomFrameRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            //Frame element = (Frame)this.Element;
            //if (element != null)
            //{
            //    element.BorderColor = Color.FromHex(App.nav_bar_color);
               
            //}
        }
       
    }
}