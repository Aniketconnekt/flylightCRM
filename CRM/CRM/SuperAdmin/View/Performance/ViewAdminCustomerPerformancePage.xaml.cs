using CRM.SuperAdmin.View.Successful;
using CRM.SuperAdmin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.SuperAdmin.View.Performance
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewAdminCustomerPerformancePage : ContentPage
	{
        AdminCustomerViewModel _adminCustomerViewModel;
        public ViewAdminCustomerPerformancePage()
		{
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent ();
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
    }
}