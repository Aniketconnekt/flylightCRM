using CRM.ViewModel;
using System;
using System.Text.RegularExpressions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.AddLead
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddLeadPage : ContentPage
    {
        AddLeadViewModel _addLeadViewModel;
        bool Flag = true;

        public AddLeadPage()
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _addLeadViewModel = new AddLeadViewModel(Navigation);
            _addLeadViewModel.Initialize();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void txt_lead_name_Focused(object sender, FocusEventArgs e)
        {
            lead_name_separator.BackgroundColor = Color.FromHex("#2BAAE1");
            txt_lead_name.TextColor = Color.FromHex("#2BAAE1");
            lead_name_unactive_image.IsVisible = false;
            lead_name_active_image.IsVisible = true;
        }
        private void txt_lead_name_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_lead_name.Text))
            {
                lead_name_unactive_image.IsVisible = true;
                lead_name_active_image.IsVisible = false;
            }
            else
            {
                txt_lead_name.TextColor = Color.FromHex("#2BAAE1");
                lead_name_unactive_image.IsVisible = false;
                lead_name_active_image.IsVisible = true;
            }
            lead_name_separator.BackgroundColor = Color.FromHex("#A1A1A1");
        }
        private void txt_company_Focused(object sender, FocusEventArgs e)
        {
            company_separator.BackgroundColor = Color.FromHex("#2BAAE1");
            txt_company.TextColor = Color.FromHex("#2BAAE1");
            company_unactive_image.IsVisible = false;
            company_active_image.IsVisible = true;
        }
        private void txt_company_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_company.Text))
            {
                company_unactive_image.IsVisible = true;
                lead_name_active_image.IsVisible = false;
            }
            else
            {
                txt_company.TextColor = Color.FromHex("#2BAAE1");
                company_unactive_image.IsVisible = false;
                company_active_image.IsVisible = true;
            }
            company_separator.BackgroundColor = Color.FromHex("#A1A1A1");
        }
        private void txt_mobile_number_Focused(object sender, FocusEventArgs e)
        {
            mobile_number_separator.BackgroundColor = Color.FromHex("#2BAAE1");
            txt_mobile_number.TextColor = Color.FromHex("#2BAAE1");
            mobile_number_unactive_image.IsVisible = false;
            mobile_number_active_image.IsVisible = true;
        }
        private void txt_mobile_number_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_mobile_number.Text))
            {
                mobile_number_unactive_image.IsVisible = true;
                lead_name_active_image.IsVisible = false;
            }
            else if (txt_mobile_number.Text.Length < 10)
            {
                //mobile_frame.BorderColor = Color.FromHex("#a1a1a1");
                txt_mobile_number.TextColor = Color.FromHex("#A1A1A1");
                mobile_number_unactive_image.IsVisible = true;
                lead_name_active_image.IsVisible = false;
            }
            else
            {
                txt_mobile_number.TextColor = Color.FromHex("#2BAAE1");
                mobile_number_unactive_image.IsVisible = false;
                mobile_number_active_image.IsVisible = true;
            }
            mobile_number_separator.BackgroundColor = Color.FromHex("#A1A1A1");
        }
        private void txt_alternate_number_Focused(object sender, FocusEventArgs e)
        {
            alternate_mobile_number_separator.BackgroundColor = Color.FromHex(App.nav_bar_color);
            txt_alternate_number.TextColor = Color.FromHex(App.nav_bar_color);
            alternate_number_unactive_image.IsVisible = false;
            alternate_number_active_image.IsVisible = true;
        }
        private void txt_alternate_number_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_alternate_number.Text))
                alternate_number_unactive_image.IsVisible = true;
            else if (txt_alternate_number.Text.Length < 10)
            {
                txt_mobile_number.TextColor = Color.FromHex("#A1A1A1");
                alternate_number_unactive_image.IsVisible = true;
            }
            else
            {
                txt_alternate_number.TextColor = Color.FromHex(App.nav_bar_color);
                alternate_number_unactive_image.IsVisible = false;
                alternate_number_active_image.IsVisible = true;
            }
            alternate_mobile_number_separator.BackgroundColor = Color.FromHex("#A1A1A1");
        }
        private void txt_address_Focused(object sender, FocusEventArgs e)
        {
            address_separator.BackgroundColor = Color.FromHex("#2BAAE1");
            txt_address.TextColor = Color.FromHex("#2BAAE1");
            address_unactive_image.IsVisible = false;
            address_active_image.IsVisible = true;
        }
        private void txt_address_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_address.Text))
            {
                address_unactive_image.IsVisible = true;
                lead_name_active_image.IsVisible = false;
            }
            else
            {
                txt_address.TextColor = Color.FromHex("#2BAAE1");
                address_unactive_image.IsVisible = false;
                address_active_image.IsVisible = true;
            }
            address_separator.BackgroundColor = Color.FromHex("#A1A1A1");
        }
        private void txt_city_Focused(object sender, FocusEventArgs e)
        {
            city_separator.BackgroundColor = Color.FromHex("#2BAAE1");
            txt_city.TextColor = Color.FromHex("#2BAAE1");
            city_unactive_image.IsVisible = false;
            city_active_image.IsVisible = true;
        }
        private void txt_city_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_city.Text))
            {
                city_unactive_image.IsVisible = true;
                lead_name_active_image.IsVisible = false;
            }
            else
            {
                txt_city.TextColor = Color.FromHex("#2BAAE1");
                city_unactive_image.IsVisible = false;
                city_active_image.IsVisible = true;
            }
            city_separator.BackgroundColor = Color.FromHex("#A1A1A1");
        }
        private void txt_district_Focused(object sender, FocusEventArgs e)
        {
            district_separator.BackgroundColor = Color.FromHex("#2BAAE1");
            txt_district.TextColor = Color.FromHex("#2BAAE1");
            district_unactive_image.IsVisible = false;
            district_active_image.IsVisible = true;
        }
        private void txt_district_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_district.Text))
            {
                district_unactive_image.IsVisible = true;
                lead_name_active_image.IsVisible = false;
            }
            else
            {
                txt_district.TextColor = Color.FromHex("#2BAAE1");
                district_unactive_image.IsVisible = false;
                district_active_image.IsVisible = true;
            }
            district_separator.BackgroundColor = Color.FromHex("#A1A1A1");
        }
        private void txt_email_Focused(object sender, FocusEventArgs e)
        {
            email_separator.BackgroundColor = Color.FromHex("#2BAAE1");
            txt_email.TextColor = Color.FromHex("#2BAAE1");
            email_unactive_image.IsVisible = false;
            email_active_image.IsVisible = true;
        }
        private void txt_email_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_email.Text))
            {
                email_unactive_image.IsVisible = true;
                lead_name_active_image.IsVisible = false;
            }
            else
            {
                txt_email.TextColor = Color.FromHex("#2BAAE1");
                email_unactive_image.IsVisible = false;
                email_active_image.IsVisible = true;
            }
            email_separator.BackgroundColor = Color.FromHex("#A1A1A1");
        }
        private void MobileNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (((Entry)sender).Text.Equals(String.Empty))
            //    Flag = true;

            //if (Flag)
            //{
            //    if (!e.NewTextValue.Equals("6") && !e.NewTextValue.Equals("7") && !e.NewTextValue.Equals("8") && !e.NewTextValue.Equals("9"))
            //    {
            //        ((Entry)sender).Text = String.Empty;
            //        return;
            //    }
            //    else
            //        Flag = false;
            //}

            try
            {
                var regex = new Regex("^[0-9]+$");
                if (!string.IsNullOrWhiteSpace(e.NewTextValue))
                {
                    if (!regex.IsMatch(e.NewTextValue))
                        ((Entry)sender).Text = e.OldTextValue;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void Txt_user_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var regex = new Regex("^[a-zA-Z0-9 ]+$");
                if (!string.IsNullOrWhiteSpace(e.NewTextValue))
                {
                    if (!regex.IsMatch(e.NewTextValue))
                        ((Entry)sender).Text = e.OldTextValue;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            date.IsOpen = !date.IsOpen;
        }
    }
}