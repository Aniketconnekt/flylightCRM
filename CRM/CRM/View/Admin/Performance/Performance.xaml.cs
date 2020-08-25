using CRM.View.Admin.Lead;
using CRM.ViewModel.AdminViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Entry = Microcharts.Entry;
using SkiaSharp;
using Microcharts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.View.Admin.Performance
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Performance : ContentPage
    {
        PerformanceViewModel _performanceViewModel;
        List<Entry> entries = new List<Entry>();

        public Performance()
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _performanceViewModel = new PerformanceViewModel(Navigation);
            _performanceViewModel.Initialize();
            MessagingCenter.Subscribe<PerformanceViewModel>(this, "ModelFilled", (sender) => BindGraph());
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
                    new Entry(_performanceViewModel.PerformanceData.LeadsCalled){Color = SKColor.Parse("#33BFCF")},
                    new Entry(_performanceViewModel.PerformanceData.Leadspending){Color = SKColor.Parse("#FF5C61")},
                    new Entry(_performanceViewModel.PerformanceData.LeadsUnCalled){Color = SKColor.Parse("#ffa973")}
                };

                ChartView.Chart = new BarChart() { Entries = entries };
            }
        }
    }
}