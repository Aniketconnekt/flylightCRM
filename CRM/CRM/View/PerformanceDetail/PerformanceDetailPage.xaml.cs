using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.PerformanceDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerformanceDetailPage : ContentPage
    {
        public PerformanceDetailPage()
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
        }
    }
}