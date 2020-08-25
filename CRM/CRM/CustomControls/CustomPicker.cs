using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CRM.CustomControls
{
    public class CustomPicker : Picker
    {
        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(CustomPicker), string.Empty);

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        //public static readonly BindableProperty ImageHeightProperty =
        //    BindableProperty.Create(nameof(ImageHeight), typeof(double), typeof(CustomPicker), 30);

        //public double ImageHeight
        //{
        //    get { return (double)GetValue(ImageHeightProperty); }
        //    set { SetValue(ImageHeightProperty, value); }
        //}

        //public static readonly BindableProperty ImageWidthProperty =
        //    BindableProperty.Create(nameof(ImageWidth), typeof(double), typeof(CustomPicker), 30);

        //public double ImageWidth
        //{
        //    get { return (double)GetValue(ImageWidthProperty); }
        //    set { SetValue(ImageWidthProperty, value); }
        //}
    }
}
