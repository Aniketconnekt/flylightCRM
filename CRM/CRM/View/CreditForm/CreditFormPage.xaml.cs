using CRM.Model;
using CRM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.CreditForm
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreditFormPage : ContentPage
    {
        CreditFormViewModel _creditFormViewModel;
        public CreditFormPage(NewLeadsData newLeadsData)
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _creditFormViewModel = new CreditFormViewModel(Navigation);
            _creditFormViewModel.Initialize(newLeadsData);
        }
    }
}