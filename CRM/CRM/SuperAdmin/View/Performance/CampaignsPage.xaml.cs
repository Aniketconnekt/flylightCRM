using CRM.SuperAdmin.Model;
using CRM.SuperAdmin.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.SuperAdmin.View.Performance
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CampaignsPage : ContentPage
	{
        CampaignsViewModel _campaignsViewModel;
        AdminCustomerData _adminCustomerData;
        public CampaignsPage (AdminCustomerData adminCustomerData)
		{
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent ();
            _adminCustomerData = adminCustomerData;
            BindingContext = _campaignsViewModel = new CampaignsViewModel(Navigation);
            _campaignsViewModel.Initialize(_adminCustomerData);
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                _campaignsViewModel.Initialize(_adminCustomerData);
        }
    }
}