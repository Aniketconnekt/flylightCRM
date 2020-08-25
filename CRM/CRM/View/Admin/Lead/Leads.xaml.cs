using CRM.Model.AdminModel;
using CRM.ViewModel.AdminViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.Admin.Lead
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Leads : ContentPage
    {
        LeadsViewModel _leadsViewModel;
        public Leads()
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _leadsViewModel = new LeadsViewModel(Navigation);
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _leadsViewModel.Initialize();
        }
        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            try
            {
                var checkbox = (CheckBox)sender;
                var user = checkbox.BindingContext as LeadsData;
                if (((CheckBox)sender).IsChecked)
                    _leadsViewModel.LeadsDataList.Add(user);
                else
                    _leadsViewModel.LeadsDataList.Remove(user);
            }
            catch (Exception ex)
            {
                DisplayAlert("", ex.Message, "Ok");
            }
        }
        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                await _leadsViewModel.Initialize();
        }
        private void SelectAll_Checked(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
                foreach (var lead in _leadsViewModel.LeadsData)
                    lead.IsSelectAllChecked = true;
            else
                foreach (var lead in _leadsViewModel.LeadsData)
                    lead.IsSelectAllChecked = false;
        }
    }
}