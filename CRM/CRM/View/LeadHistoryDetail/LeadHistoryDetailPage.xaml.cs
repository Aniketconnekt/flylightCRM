using CRM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.LeadHistoryDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeadHistoryDetailPage : ContentPage
    {
        LeadHistoryViewModel viewModel;
        public LeadHistoryDetailPage()
        {
            try
            {
                NavigationPage.SetBackButtonTitle(this, "");
                InitializeComponent();
                BindingContext = viewModel = new LeadHistoryViewModel(Navigation);
                //_list.ItemsSource = viewModel.list;
            }
            catch (Exception ex)
            {
            }
        }
    }
}