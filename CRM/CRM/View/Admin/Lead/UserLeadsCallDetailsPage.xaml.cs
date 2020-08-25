using CRM.ViewModel.AdminViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.Admin.Lead
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserLeadsCallDetailsPage : ContentPage
	{
        UserLeadsCallDetailsViewModel _userLeadsCallDetailsViewModel;
        public UserLeadsCallDetailsPage ()
		{
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent ();
            BindingContext = _userLeadsCallDetailsViewModel = new UserLeadsCallDetailsViewModel(Navigation);
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _userLeadsCallDetailsViewModel.Initialize();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                _userLeadsCallDetailsViewModel.SearchCommand.Execute(null);
        }
    }
}