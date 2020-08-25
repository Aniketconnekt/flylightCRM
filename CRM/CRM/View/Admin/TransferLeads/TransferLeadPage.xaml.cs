using CRM.ViewModel.AdminViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.Admin.TransferLeads
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TransferLeadPage : ContentPage
	{
        TransferLeadViewModel _transferLeadViewModel;
        public TransferLeadPage ()
		{
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _transferLeadViewModel = new TransferLeadViewModel(Navigation);
            _transferLeadViewModel.Initialize();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}