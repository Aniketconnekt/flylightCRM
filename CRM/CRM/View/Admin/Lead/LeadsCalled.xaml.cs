using CRM.Model;
using CRM.SuperAdmin.Model;
using CRM.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.Admin.Lead
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeadsCalled : ContentPage
    {
        LeadsCalledViewModel _leadsCalledViewModel;
        public LeadsCalled(int count)
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _leadsCalledViewModel = new LeadsCalledViewModel(Navigation);
            _leadsCalledViewModel.Initialize(count);
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            ((ListView)sender).SelectedItem = null;
        }
    }
}