using CRM.Model;
using CRM.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.UpdateInformation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateLeadInformation : ContentPage
    {
        UpdateInformationViewModel _updateInformationViewModel;
        public UpdateLeadInformation(NewLeadsData newLeadsData)
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _updateInformationViewModel = new UpdateInformationViewModel(Navigation);
            _updateInformationViewModel.Initialize(newLeadsData);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            date.IsOpen = !date.IsOpen;
        }
    }
}