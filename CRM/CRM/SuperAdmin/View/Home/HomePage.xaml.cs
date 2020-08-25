using CRM.Interfaces;
using CRM.SuperAdmin.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.SuperAdmin.View.Home

{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        HomeViewModel _homeViewModel;
        public HomePage()
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _homeViewModel = new HomeViewModel(Navigation);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _homeViewModel.Initialize();
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async()=> {
                var result = await DisplayAlert("", "Do you want to exist?", "Yes", "No");
                if (result)
                    DependencyService.Get<IAppClosed>().CloseApp();
            });
            return true;
        }
    }
}