using CRM.ViewModel.AdminViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.Admin.Status
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StatusPage : ContentPage
	{
        StatusViewModel _statusViewModel;
        public StatusPage ()
		{
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent ();
            BindingContext = _statusViewModel = new StatusViewModel(Navigation);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _statusViewModel.Initialize();
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            ((ListView)sender).SelectedItem = null;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}