using CRM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace CRM.View.ResetPassword
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetPassword : ContentPage
    {
        ForgetPasswordViewModel _forgetPasswordViewModel;
        public ResetPassword(string mobileno)
        {
            InitializeComponent();
            // iOS Platform
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            BindingContext = _forgetPasswordViewModel = new ForgetPasswordViewModel(Navigation);
            _forgetPasswordViewModel.Initialize(mobileno);
        }
        private void txt_new_pass_Focused(object sender, FocusEventArgs e)
        {
            new_pass_frame.BorderColor = Color.FromHex("#2baae1");
            img_new_pass_inactive.IsVisible = false;
            img_new_pass_active.IsVisible = true;
        }
        private void txt_new_pass_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_new_pass.Text))
            {
                new_pass_frame.BorderColor = Color.FromHex("#a1a1a1");
                img_new_pass_inactive.IsVisible = true;
                img_new_pass_active.IsVisible = false;
            }
            else
            {
                new_pass_frame.BorderColor = Color.FromHex("#2baae1");
                img_new_pass_inactive.IsVisible = false;
                img_new_pass_active.IsVisible = true;
            }
        }
        private void txt_confirm_pass_Focused(object sender, FocusEventArgs e)
        {
            confirm_pass_frame.BorderColor = Color.FromHex("#2baae1");
            img_confirm_pass_inactive.IsVisible = false;
            img_confirm_pass_active.IsVisible = true;
        }
        private void txt_confirm_pass_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_confirm_pass.Text))
            {
                confirm_pass_frame.BorderColor = Color.FromHex("#a1a1a1");
                img_confirm_pass_inactive.IsVisible = true;
                img_confirm_pass_active.IsVisible = false;
            }
            else
            {
                confirm_pass_frame.BorderColor = Color.FromHex("#2baae1");
                img_confirm_pass_inactive.IsVisible = false;
                img_confirm_pass_active.IsVisible = true;
            }
        }
    }
}