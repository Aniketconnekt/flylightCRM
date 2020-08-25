using CRM.Model;
using CRM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.TransferLead
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransferLeadPage : ContentPage
    {
        TransferLeadViewModel _transferLeadViewModel;
        List<NewLeadsData> _newLeadsData;
        public TransferLeadPage(List<NewLeadsData> newLeadsDataList)
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            _newLeadsData = newLeadsDataList;
            BindingContext = _transferLeadViewModel = new TransferLeadViewModel(Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _transferLeadViewModel.Initialize(_newLeadsData);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}