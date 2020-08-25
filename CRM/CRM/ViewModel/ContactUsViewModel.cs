using CRM.Common.Command;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.ViewModel
{
    public class ContactUsViewModel : BaseViewModel
    {
        #region CTOR
        public ContactUsViewModel(INavigation navigation) : base(navigation)
        {
            _callno = CRM.Common.Helpers.Settings.CRM_CallNo;
            _whatsappNo = CRM.Common.Helpers.Settings.CRM_WhatsappNo;
        }
        #endregion

        #region Commands
        public DelegateCommand CallCommand => new DelegateCommand(ExecuteOnCall);
        public DelegateCommand WhatsAppCommand => new DelegateCommand(ExecuteOnWhatsApp);
        #endregion

        #region Properties
        private string _callno;
        public string callno
        {
            get => _callno;
            set { _callno = value; OnPropertyChanged(); }
        }
        private string _whatsappNo;
        public string whatsappNo
        {
            get => _whatsappNo;
            set { _whatsappNo = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async void ExecuteOnCall(object obj)
        {
            try
            {
                PhoneDialer.Open(callno);
            }
            catch (Exception ex)
            {
                await ShowAlert(ex.Message);
            }
        }
        public async void ExecuteOnWhatsApp(object obj)
        {
            try
            {
                await Browser.OpenAsync(new Uri("whatsapp://send?phone=" + whatsappNo));
            }
            catch (Exception ex)
            {
                await ShowAlert(ex.Message);
            }
        }
        #endregion
    }
}
