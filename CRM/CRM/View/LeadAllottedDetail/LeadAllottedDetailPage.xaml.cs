using CRM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.LeadAllottedDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeadAllottedDetailPage : ContentPage
    {
        LeadAllottedViewModel viewModel;
        public LeadAllottedDetailPage()
        {
            try
            {
                NavigationPage.SetBackButtonTitle(this, "");
                InitializeComponent();
                BindingContext = viewModel = new LeadAllottedViewModel(Navigation);
                //_list.ItemsSource = viewModel.list;
            }
            catch (Exception ex)
            {
            }
        }
    }
}