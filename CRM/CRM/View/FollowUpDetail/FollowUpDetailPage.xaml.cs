using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.FollowUpDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FollowUpDetailPage : ContentPage
    {
        FollowUpViewModel viewModel;
        public FollowUpDetailPage()
        {
            try
            {
                NavigationPage.SetBackButtonTitle(this, "");
                InitializeComponent();
                BindingContext = viewModel = new FollowUpViewModel(Navigation);
                //_list.ItemsSource = viewModel.list;
            }
            catch (Exception ex)
            {
            }
        }
    }
}