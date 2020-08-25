using CRM.SuperAdmin.Model;
using CRM.SuperAdmin.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.SuperAdmin.View.Performance
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MoreDetailPage : ContentPage
	{
        MoreDetailViewModel _moreDetailViewModel;
        AdminCustomerData _adminCustomerData;
        public MoreDetailPage (AdminCustomerData adminCustomerData)
		{
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent ();
            _adminCustomerData = adminCustomerData;
            BindingContext = _moreDetailViewModel = new MoreDetailViewModel(Navigation);
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _moreDetailViewModel.Initialize(_adminCustomerData);
        }
    }
}