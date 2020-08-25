using CRM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Entry = Microcharts.Entry;
using SkiaSharp;
using Microcharts;

namespace CRM.View.Performance
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerformancePage : ContentPage
    {
        PerformanceViewModel _performanceViewModel;
        List<Entry> entries = new List<Entry>();
        public PerformancePage()
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _performanceViewModel = new PerformanceViewModel(Navigation);
            MessagingCenter.Subscribe<PerformanceViewModel>(this, "UserModelFilled", (sender) => BindGraph());
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _performanceViewModel.Initialize();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void BindGraph()
        {
            if (_performanceViewModel.PerformanceData != null)
            {
                entries = new List<Entry>
                {
                    new Entry(_performanceViewModel.PerformanceData.TotalLeads){Color = SKColor.Parse("#33becf")},
                    new Entry(_performanceViewModel.PerformanceData.LeadsCalled){Color = SKColor.Parse("#ffa973")},
                    new Entry(_performanceViewModel.PerformanceData.Leadspending){Color = SKColor.Parse("#ff5c61")},
                    new Entry(_performanceViewModel.PerformanceData.LeadsUnCalled){Color = SKColor.Parse("#18d55e")}
                };

                ChartView.Chart = new BarChart() { Entries = entries };
            }
        }
    }
}