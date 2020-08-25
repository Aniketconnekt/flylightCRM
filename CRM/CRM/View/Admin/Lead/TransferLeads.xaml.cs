using CRM.Model.AdminModel;
using CRM.ViewModel.AdminViewModel;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.Admin.Lead
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransferLeads : ContentPage
    {
        TransferLeadsViewModel _transferLeadsViewModel;
        public TransferLeads(List<LeadDetailsList> leadDetailsLists, int userID)
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _transferLeadsViewModel = new TransferLeadsViewModel(Navigation);
            _transferLeadsViewModel.Initialize(leadDetailsLists, userID);
        }
    }
}