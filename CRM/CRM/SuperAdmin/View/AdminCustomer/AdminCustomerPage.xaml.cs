using CRM.SuperAdmin.View.AddAdminCustomer;
using CRM.SuperAdmin.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.SuperAdmin.View.AdminCustomer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminCustomerPage : ContentPage
    {
        AdminCustomerViewModel _adminCustomerViewModel;
        public AdminCustomerPage()
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _adminCustomerViewModel = new AdminCustomerViewModel(Navigation);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _adminCustomerViewModel.Initialize();
        }
        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            ((ListView)sender).SelectedItem = null;
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                await _adminCustomerViewModel.Initialize();
        }
    }
}