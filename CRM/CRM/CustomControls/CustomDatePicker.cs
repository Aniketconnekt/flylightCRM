using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CRM.CustomControls
{
    public class CustomDatePicker : DatePicker
    {
        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(CustomPicker), string.Empty);

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }
    }
}
