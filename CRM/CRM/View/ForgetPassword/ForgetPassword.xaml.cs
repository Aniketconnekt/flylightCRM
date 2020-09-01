using CRM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace CRM.View.ForgetPassword
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgetPassword : ContentPage
    {
        ForgetPasswordViewModel _forgetPasswordViewModel;
        public ForgetPassword()
        {
            InitializeComponent();
            // iOS Platform
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            BindingContext = _forgetPasswordViewModel = new ForgetPasswordViewModel(Navigation);
        }

        private void txtMobileNumber_Focused(object sender, FocusEventArgs e)
        {
            mobile_frame.BorderColor = Color.FromHex("#2baae1");
            txtMobileNumber.TextColor = Color.FromHex("#2baae1");
            img_mobile_inactive.IsVisible = false;
            img_mobile_active.IsVisible = true;
        }
        private void txtMobileNumber_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(txtMobileNumber.Text))
            {
                mobile_frame.BorderColor = Color.FromHex("#a1a1a1");
                img_mobile_inactive.IsVisible = true;
                img_mobile_active.IsVisible = false;
            }
            else if (txtMobileNumber.Text.Length < 10)
            {
                mobile_frame.BorderColor = Color.FromHex("#a1a1a1");
                txtMobileNumber.TextColor = Color.FromHex("#a1a1a1");
                img_mobile_inactive.IsVisible = true;
                img_mobile_active.IsVisible = false;
            }
            else
            {
                mobile_frame.BorderColor = Color.FromHex("#2baae1");
                txtMobileNumber.TextColor = Color.FromHex("#2baae1");
                img_mobile_inactive.IsVisible = false;
                img_mobile_active.IsVisible = true;
            }
        }

        private void txtOtp_Focused(object sender, FocusEventArgs e)
        {
            otp_frame.BorderColor = Color.FromHex("#2baae1");
        }
        private void txtOtp_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(txtOtp.Text))
                otp_frame.BorderColor = Color.FromHex("#a1a1a1");
            else if (txtOtp.Text.Length < 4)
            {
                otp_frame.BorderColor = Color.FromHex("#a1a1a1");
                txtOtp.TextColor = Color.FromHex("#a1a1a1");
            }
            else
                otp_frame.BorderColor = Color.FromHex("#2baae1");
        }
    }
}