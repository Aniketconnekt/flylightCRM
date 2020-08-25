using CRM.Model.AdminModel;
using CRM.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.Admin.Lead
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeadsCalledAddLead : ContentPage
    {
        LeadDetailsViewModel _leadDetailsViewModel;
        public LeadsCalledAddLead(int userId)
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _leadDetailsViewModel = new LeadDetailsViewModel(Navigation);
            _leadDetailsViewModel.Initialize(userId, 7);
        }

        private void Calledlist_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            try
            {
                var checkbox = (CheckBox)sender;
                var lead = checkbox.BindingContext as LeadDetailsList;
                if (((CheckBox)sender).IsChecked)
                    _leadDetailsViewModel.TransferLeadsList.Add(lead);
                else
                    _leadDetailsViewModel.TransferLeadsList.Remove(lead);
            }
            catch (Exception ex)
            {
                DisplayAlert("", ex.Message, "Ok");
            }
        }

        private void Pendinglist_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            try
            {
                var checkbox = (CheckBox)sender;
                var lead = checkbox.BindingContext as LeadDetailsList;
                if (((CheckBox)sender).IsChecked)
                    _leadDetailsViewModel.TransferLeadsList.Add(lead);
                else
                    _leadDetailsViewModel.TransferLeadsList.Remove(lead);
            }
            catch (Exception ex)
            {
                DisplayAlert("", ex.Message, "Ok");
            }
        }
    }
}