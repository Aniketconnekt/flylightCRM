using CRM.Model;
using CRM.ViewModel.AdminViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.Admin.Lead
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactsPage : ContentPage
	{
        public ContactsViewModel _contactsViewModel;
        public ContactsPage ()
		{
			InitializeComponent ();
            BindingContext = _contactsViewModel = new ContactsViewModel(Navigation);
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            ((ListView)sender).SelectedItem = null;
        }
    }
}