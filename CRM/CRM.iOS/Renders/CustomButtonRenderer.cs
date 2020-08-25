using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.iOS.Renders;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Button), typeof(CustomButtonRenderer))]
namespace CRM.iOS.Renders
{
    public class CustomButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                var button = (Button)Element;
                button.BackgroundColor = Color.FromHex(App.nav_bar_color);
            }
        }
    }
}