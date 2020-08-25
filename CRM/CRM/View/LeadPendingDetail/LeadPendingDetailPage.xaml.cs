using CRM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.LeadPendingDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeadPendingDetailPage : ContentPage
    {
        LeadPendingDetailViewModel viewModel;
        public LeadPendingDetailPage()
        {
            try
            {
                NavigationPage.SetBackButtonTitle(this, "");
                InitializeComponent();
                BindingContext = viewModel = new LeadPendingDetailViewModel(Navigation);
                _list.ItemsSource = viewModel.list;
            }
            catch (Exception ex)
            {
            }
        }
    }
}