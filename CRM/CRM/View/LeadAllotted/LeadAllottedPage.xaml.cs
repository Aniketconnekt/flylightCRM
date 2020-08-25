using CRM.Model;
using CRM.View.UpdateInformation;
using CRM.ViewModel;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.LeadAllotted
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeadAllottedPage : ContentPage
    {
        LeadAllottedViewModel _leadAllottedViewModel;
        public LeadAllottedPage(NewCampaignData selectedCampaign)
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _leadAllottedViewModel = new LeadAllottedViewModel(Navigation);
            _leadAllottedViewModel.Initialize(selectedCampaign);
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
    }
}