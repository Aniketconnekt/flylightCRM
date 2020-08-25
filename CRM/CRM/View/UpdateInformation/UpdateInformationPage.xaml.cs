using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CRM.Model;
using CRM.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.UpdateInformation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateInformationPage : ContentPage
    {
        UpdateLeadInfoViewModel _updateLeadInfoViewModel;
        //bool Flag = true;

        public UpdateInformationPage(List<NewLeadsData> newLeadsDatas, string updateType)
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _updateLeadInfoViewModel = new UpdateLeadInfoViewModel(Navigation);
            _updateLeadInfoViewModel.Initialize(newLeadsDatas,updateType);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
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