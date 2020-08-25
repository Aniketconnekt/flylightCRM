using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CRM.Model.AdminModel;
using CRM.ViewModel.AdminViewModel;

namespace CRM.View.Admin.Compaign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Campaigns : ContentPage
    {
        CampaignViewModel _campaignViewModel;
        public Campaigns()
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _campaignViewModel = new CampaignViewModel(Navigation);
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _campaignViewModel.Initialize();
        }
        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            try
            {
                var checkbox = (CheckBox)sender;
                var campaign = checkbox.BindingContext as CampaignData;
                if (((CheckBox)sender).IsChecked)
                    _campaignViewModel.CampaignDataList.Add(campaign);
                else
                    _campaignViewModel.CampaignDataList.Remove(campaign);
            }
            catch (Exception ex)
            {
                DisplayAlert("", ex.Message, "Ok");
            }
        }
        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                await _campaignViewModel.Initialize();
        }
        private void SelectAll_Checked(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
                foreach (var camp in _campaignViewModel.CampaignData)
                    camp.IsSelectAllChecked = true;
            else
                foreach (var camp in _campaignViewModel.CampaignData)
                    camp.IsSelectAllChecked = false;
        }
    }
}