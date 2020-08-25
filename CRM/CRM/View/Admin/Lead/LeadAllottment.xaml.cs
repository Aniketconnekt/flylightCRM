using CRM.Model;
using CRM.Model.AdminModel;
using CRM.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.Admin.Lead
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeadAllottment : ContentPage
    {
        LeadAllottmentViewModel _leadAllottmentViewModel;
        public LeadAllottment()
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _leadAllottmentViewModel = new LeadAllottmentViewModel(Navigation);
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _leadAllottmentViewModel.Initialize();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            try
            {
                var checkbox = (CheckBox)sender;
                var allot = checkbox.BindingContext as LeadAllottmentData;
                if (((CheckBox)sender).IsChecked)
                    _leadAllottmentViewModel.LeadAllottmentDataList.Add(allot);
                else
                    _leadAllottmentViewModel.LeadAllottmentDataList.Remove(allot);
            }
            catch (Exception ex)
            {
                DisplayAlert("", ex.Message, "Ok");
            }
        }
        private void SelectAll_Checked(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                foreach (var lead in _leadAllottmentViewModel.LeadAllottmentData)
                    lead.IsSelectAllChecked = true;
            }
            else
            {
                foreach (var lead in _leadAllottmentViewModel.LeadAllottmentData)
                    lead.IsSelectAllChecked = false;
            }
        }
    }
}