using CRM.Interfaces;
using CRM.Model;
using CRM.View.UpdateInformation;
using CRM.ViewModel;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.Lead
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeadPage : ContentPage
    {
        LeadViewModel _leadViewModel;
        public LeadPage()
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _leadViewModel = new LeadViewModel(Navigation);
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _leadViewModel.Initialize();
        }
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () => {
                var result = await DisplayAlert("", "Do you want to exist?", "Yes", "No");
                if (result)
                    DependencyService.Get<IAppClosed>().CloseApp();
            });
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
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UpdateLeadInformation((NewLeadsData)((TappedEventArgs)e).Parameter));
        }
    }
}