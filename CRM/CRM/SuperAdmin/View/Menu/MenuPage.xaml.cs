using CRM.SuperAdmin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.SuperAdmin.View.Menu
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuPage : ContentPage
	{
        MasterDetailViewModel viewModel;
        public ListView ListView { get { return menu_list; } }

        public MenuPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new MasterDetailViewModel(Navigation);
            menu_list.ItemsSource = viewModel.list;
            GrdMenuTitle.BackgroundColor = Color.FromHex(App.nav_bar_color);
            var userId = SecureStorage.GetAsync("UserId");
            imgBackground.Source = userId.Result == "3" ? "background.png" : "background.png";
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            ((ListView)sender).SelectedItem = null;
        }
    }
}