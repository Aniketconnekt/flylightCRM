using CRM.Model;
using CRM.View.UpdateInformation;
using CRM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.LeadHistory
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeadHistoryPage : ContentPage
    {
        LeadHistoryViewModel _leadHistoryViewModel;
        public LeadHistoryPage()
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _leadHistoryViewModel = new LeadHistoryViewModel(Navigation);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _leadHistoryViewModel.Initialize();
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