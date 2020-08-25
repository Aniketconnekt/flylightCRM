using CRM.Model;
using CRM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.SendReminder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendReminderPage : ContentPage
    {
        SendReminderViewModel _sendReminderViewModel;
        public SendReminderPage(NewLeadsData newLeadsData)
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            BindingContext = _sendReminderViewModel = new SendReminderViewModel(Navigation);
            _sendReminderViewModel.Initialize(newLeadsData);
        }
    }
}