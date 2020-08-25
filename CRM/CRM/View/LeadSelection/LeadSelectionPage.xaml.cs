using System;
using Acr.UserDialogs;
using CRM.Model;
using CRM.View.UpdateInformation;
using CRM.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.LeadSelection
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeadSelectionPage : ContentPage
    {
        LeadSelectionViewModel _leadSelectionViewModel;
        Tuple<int, int, string, string> _tuple = null;

        public LeadSelectionPage(Tuple<int, int, string, string> tuple)
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            _tuple = tuple;
            BindingContext = _leadSelectionViewModel = new LeadSelectionViewModel(Navigation);
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _leadSelectionViewModel.Initialize(_tuple);
        }
        private void btnSubmit_Clicked(object sender, EventArgs e)
        {
            if (edit_active.IsVisible == true)
                _leadSelectionViewModel.EditLeadCommand.Execute(null);
            else if (delete_active.IsVisible == true)
                _leadSelectionViewModel.DeleteLeadCommand.Execute(null);
            else if (allotted_to_user_active.IsVisible == true)
                _leadSelectionViewModel.TransferLeadCommand.Execute(null);
        }
        private void AllottedToUserClicked(object sender, EventArgs e)
        {
            if (allotted_to_user_active.IsVisible)
            {
            }
            else
            {
                allotted_to_user_active.IsVisible = true;
                allotted_to_user_unactive.IsVisible = false;
            }
            delete_active.IsVisible = false;
            delete_unactive.IsVisible = true;

            edit_active.IsVisible = false;
            edit_unactive.IsVisible = true;
        }
        private void DeleteClicked(object sender, EventArgs e)
        {
            if (delete_active.IsVisible)
            {
            }
            else
            {
                delete_active.IsVisible = true;
                delete_unactive.IsVisible = false;
            }
            allotted_to_user_active.IsVisible = false;
            allotted_to_user_unactive.IsVisible = true;

            edit_active.IsVisible = false;
            edit_unactive.IsVisible = true;
        }
        private void EditClicked(object sender, EventArgs e)
        {
            if (edit_active.IsVisible)
            {
            }
            else
            {
                edit_active.IsVisible = true;
                edit_unactive.IsVisible = false;
            }
            delete_active.IsVisible = false;
            delete_unactive.IsVisible = true;

            allotted_to_user_active.IsVisible = false;
            allotted_to_user_unactive.IsVisible = true;
        }
        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            try
            {
                var checkbox = (CheckBox)sender;
                var user = checkbox.BindingContext as NewLeadsData;
                if (((CheckBox)sender).IsChecked)
                    _leadSelectionViewModel.UsersDatasList.Add(user);
                else
                    _leadSelectionViewModel.UsersDatasList.Remove(user);
            }
            catch (Exception ex)
            {
                DisplayAlert("", ex.Message, "Ok");
            }
        }
        private async void PhoneCall_Tapped(object sender, EventArgs e)
        {
            try
            {
                if(!string.IsNullOrWhiteSpace(((NewLeadsData)((TappedEventArgs)e).Parameter).MobileNumber))
                    PhoneDialer.Open(((NewLeadsData)((TappedEventArgs)e).Parameter).MobileNumber);

                await Navigation.PushAsync(new UpdateLeadInformation((NewLeadsData)((TappedEventArgs)e).Parameter));
            }
            catch (Exception ex)
            {
                await DisplayAlert("", ex.Message, "Ok"); 
            }
        }
        private void SelectAll_Checked(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                foreach (var lead in _leadSelectionViewModel.LeadsData)
                    lead.IsSelectAllChecked = true;
            }
            else
            {
                foreach (var lead in _leadSelectionViewModel.LeadsData)
                    lead.IsSelectAllChecked = false;
            }
        }
        private async void CopyNumber_Tapped(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync(((NewLeadsData)((TappedEventArgs)e).Parameter).MobileNumber);
            UserDialogs.Instance.Toast("Mobile number copied to clipboard.", TimeSpan.FromSeconds(2));
        }
    }
}