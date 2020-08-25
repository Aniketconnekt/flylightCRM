using CRM.SuperAdmin.Model;
using CRM.ViewModel.AdminViewModel;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.Admin.Users
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Users : ContentPage
    {
        UserViewModel _userViewModel;
        public Users()
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _userViewModel = new UserViewModel(Navigation);
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _userViewModel.Initialize();
        }
        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            try
            {
                var checkbox = (CheckBox)sender;
                var user = checkbox.BindingContext as UsersData;
                if (((CheckBox)sender).IsChecked)
                    _userViewModel.UsersDatasList.Add(user);
                else
                    _userViewModel.UsersDatasList.Remove(user);
            }
            catch (Exception ex)
            {
                DisplayAlert("", ex.Message, "Ok");
            }
        }
        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                await _userViewModel.Initialize();
        }
        private void SelectAll_Checked(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
                foreach (var user in _userViewModel.UsersData)
                    user.IsSelectAllChecked = true;
            else
                foreach (var user in _userViewModel.UsersData)
                    user.IsSelectAllChecked = false;
        }
    }
}