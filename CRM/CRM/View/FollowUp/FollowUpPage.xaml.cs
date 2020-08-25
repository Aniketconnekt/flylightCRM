using CRM.Model;
using CRM.View.UpdateInformation;
using CRM.ViewModel;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.FollowUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FollowUpPage : ContentPage
    {
        FollowUpViewModel _followUpViewModel;
        public FollowUpPage()
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _followUpViewModel = new FollowUpViewModel(Navigation);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _followUpViewModel.Initialize();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        private async void PhoneCall_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(((NewLeadsData)((TappedEventArgs)e).Parameter).MobileNumber))
                    PhoneDialer.Open(((NewLeadsData)((TappedEventArgs)e).Parameter).MobileNumber);

                await Navigation.PushAsync(new UpdateLeadInformation((NewLeadsData)((TappedEventArgs)e).Parameter));
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