using CRM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.LeadCalledDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeadCalledDetailPage : ContentPage
    {
        LeadCalledViewModel viewModel;
        public LeadCalledDetailPage()
        {
            try
            {
                NavigationPage.SetBackButtonTitle(this, "");
                InitializeComponent();
                BindingContext = viewModel = new LeadCalledViewModel(Navigation);
                //_list.ItemsSource = viewModel.list;
            }
            catch (Exception ex)
            {
            }
        }
    }
}