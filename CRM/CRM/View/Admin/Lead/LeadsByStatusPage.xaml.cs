using CRM.ViewModel.AdminViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.Admin.Lead
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeadsByStatusPage : ContentPage
    {
        LeadsByStatusViewModel _leadsByStatusViewModel;
        public LeadsByStatusPage()
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _leadsByStatusViewModel = new LeadsByStatusViewModel(Navigation);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _leadsByStatusViewModel.Initialize();
        }
    }
}