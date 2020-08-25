using CRM.View.LeadSelection;
using CRM.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.SearchLead
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchLeadPage : ContentPage
    {
        SearchLeadViewModel _searchLeadViewModel;
        bool Flag = true;

        public SearchLeadPage()
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _searchLeadViewModel = new SearchLeadViewModel(Navigation);
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _searchLeadViewModel.Initialize();
        }
        private void txtMobileNumber_Focused(object sender, FocusEventArgs e)
        {
            mobile_frame.BorderColor = Color.FromHex("#2baae1");
            txtMobileNumber.TextColor = Color.FromHex("#2baae1");
        }
        private void txtMobileNumber_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(txtMobileNumber.Text))
                mobile_frame.BorderColor = Color.FromHex("#a1a1a1");
            else
            {
                mobile_frame.BorderColor = Color.FromHex("#2baae1");
                txtMobileNumber.TextColor = Color.FromHex("#2baae1");
            }
        }
        private void txtName_Focused(object sender, FocusEventArgs e)
        {
            name_frame.BorderColor = Color.FromHex("#2baae1");
            txtName.TextColor = Color.FromHex("#2baae1");
        }
        private void txtName_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
                name_frame.BorderColor = Color.FromHex("#a1a1a1");
            else if (txtName.Text.Length < 10)
            {
                name_frame.BorderColor = Color.FromHex("#a1a1a1");
                txtName.TextColor = Color.FromHex("#a1a1a1");
            }
            else
            {
                name_frame.BorderColor = Color.FromHex("#2baae1");
                txtName.TextColor = Color.FromHex("#2baae1");
            }
        }
        private void MobileNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((Entry)sender).Text.Equals(String.Empty))
                Flag = true;

            if (Flag)
            {
                if (!e.NewTextValue.Equals("6") && !e.NewTextValue.Equals("7") && !e.NewTextValue.Equals("8") && !e.NewTextValue.Equals("9"))
                {
                    ((Entry)sender).Text = String.Empty;
                    return;
                }
                else
                    Flag = false;
            }
        }
    }
}