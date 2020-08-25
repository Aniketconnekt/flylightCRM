using CRM.Model;
using CRM.SuperAdmin.Model;
using CRM.SuperAdmin.View.Successful;
using CRM.SuperAdmin.ViewModel;
using CRM.ViewModel;
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
	public partial class UsersPage : ContentPage
	{
        UsersViewModel _usersViewModel;
        public UsersPage (AdminCustomerData adminCustomerData)
		{
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent ();
            BindingContext = _usersViewModel = new UsersViewModel(Navigation);
            _usersViewModel.Initialize(adminCustomerData);
        }
    }
}