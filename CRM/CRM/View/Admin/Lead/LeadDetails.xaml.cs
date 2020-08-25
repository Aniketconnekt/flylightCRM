using CRM.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.Admin.Lead
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeadDetails : ContentPage
    {
        LeadDetailsViewModel _leadDetailsViewModel;
        public LeadDetails(int userID, int count)
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _leadDetailsViewModel = new LeadDetailsViewModel(Navigation);
            _leadDetailsViewModel.Initialize(userID, count);
        }
    }
}