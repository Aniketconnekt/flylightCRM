using CRM.Common.Constants;
using CRM.Common.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : ContentPage
    {
        CRM.ViewModel.MasterDetailViewModel viewModel;
        public ListView ListView { get { return menu_list; } }
        public MasterPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new CRM.ViewModel.MasterDetailViewModel(Navigation);
            menu_list.ItemsSource = viewModel.list;
            GrdMenuTitle.BackgroundColor =Color.FromHex(App.nav_bar_color);
            var roleId = Settings.CRM_UserRole; //SecureStorage.GetAsync(AppConstant.UserRole);
            imgBackground.Source = roleId == "2" ? "background.png" : "background.png";
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            ((ListView)sender).SelectedItem = null;
        }
    }
}