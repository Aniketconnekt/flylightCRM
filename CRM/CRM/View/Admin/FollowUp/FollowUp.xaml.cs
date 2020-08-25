using CRM.Model;
using CRM.ViewModel.AdminViewModel;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.Admin.FollowUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FollowUp : ContentPage
	{
        FollowUpViewModel _followUpViewModel;
        public FollowUp ()
		{
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent ();
            BindingContext = _followUpViewModel = new FollowUpViewModel(Navigation);
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _followUpViewModel.Initialize();
        }
        private async void PhoneCall_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(((NewLeadsData)((TappedEventArgs)e).Parameter).MobileNumber))
                    PhoneDialer.Open(((NewLeadsData)((TappedEventArgs)e).Parameter).MobileNumber);
            }
            catch (Exception ex)
            {
                await DisplayAlert("", ex.Message, "Ok");
            }
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            date.IsOpen = !date.IsOpen;
        }
        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            endDate.IsOpen = !endDate.IsOpen;
        }
    }
}