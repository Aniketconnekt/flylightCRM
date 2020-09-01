using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.iOS.Renders;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SearchBar), typeof(FixedSearchBarRenderer))]
namespace CRM.iOS.Renders
{
    public class FixedSearchBarRenderer : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0) && Control != null)
            {
                Control.SearchTextField.BackgroundColor = UIColor.FromRGB(255, 255, 255);

            }
        }
    }
}