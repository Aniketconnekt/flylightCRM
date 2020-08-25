using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CRM.ViewModel.AdminViewModel;
using CRM.Interfaces;

namespace CRM.View.Admin.Lead
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Lead : ContentPage
    {
        LeadViewModel _leadViewModel;
        public Lead()
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _leadViewModel = new LeadViewModel(Navigation);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _leadViewModel.Initialize();
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () => {
                var result = await DisplayAlert("", "Do you want to exist?", "Yes", "No");
                if (result)
                    DependencyService.Get<IAppClosed>().CloseApp();
            });
            return true;
        }
    }
}