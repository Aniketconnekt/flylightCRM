using CRM.SuperAdmin.ViewModel;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Entry = Microcharts.Entry;
using SkiaSharp;
using Microcharts;

namespace CRM.SuperAdmin.View.Performance
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerformancePage : ContentPage
    {
        HomeViewModel _homeViewModel;
        List<Entry> entries = new List<Entry>();

        public PerformancePage()
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _homeViewModel = new HomeViewModel(Navigation);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _homeViewModel.Initialize();

            if(_homeViewModel.HomeData != null)
            {
                entries = new List<Entry>
                {
                    new Entry(_homeViewModel.HomeData.TotalLead)
                    { Color = SKColor.Parse("#ff5c61")},
                    new Entry(_homeViewModel.HomeData.UserCount)
                    { Color = SKColor.Parse("#33becf")}
                };
                ChartView.Chart = new DonutChart() { Entries = entries};
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewAdminCustomerPerformancePage());
        }
    }
    public class Person
    {
        public string Name { get; set; }
        public double Height { get; set; }
    }
}