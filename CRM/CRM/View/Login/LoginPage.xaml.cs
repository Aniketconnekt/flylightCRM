using Acr.UserDialogs;
using CRM.CustomControls;
using CRM.Model;
using CRM.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel _loginViewModel;
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = _loginViewModel = new LoginViewModel(Navigation);
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

        private void txtPassword_Focused(object sender, FocusEventArgs e)
        {
            password_frame.BorderColor = Color.FromHex("#2baae1");
            img_password_inactive.IsVisible = false;
            img_password_active.IsVisible = true;
        }

        private void txtPassword_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                password_frame.BorderColor = Color.FromHex("#a1a1a1");
                img_password_inactive.IsVisible = true;
                img_password_active.IsVisible = false;
            }
            else
            {
                password_frame.BorderColor = Color.FromHex("#2baae1");
                img_password_inactive.IsVisible = false;
                img_password_active.IsVisible = true;
            }
        }
    }
}