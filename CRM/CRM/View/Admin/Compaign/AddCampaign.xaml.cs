using CRM.Model.AdminModel;
using CRM.ViewModel.AdminViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.Admin.Compaign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCampaign : ContentPage
    {
        AddCampaignViewModel _addCampaignViewModel;
        public AddCampaign(CampaignData campaignData)
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _addCampaignViewModel = new AddCampaignViewModel(Navigation);
            _addCampaignViewModel.Initialize(campaignData);
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}