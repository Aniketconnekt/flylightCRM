using CRM.Model;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CRM.ViewModel
{
    class ResetPasswordViewModel : BaseViewModel
    {
        string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChanged("NewPassword");
            }
        }

        string _confPassword;
        public string ConfPassword
        {
            get => _confPassword;
            set
            {
                _confPassword = value;
                OnPropertyChanged("ConfPassword");
            }
        }

        long _MobileNo;
        public ResetPasswordViewModel(long MobileNo,INavigation navigation) : base(navigation)
        {
            _MobileNo = MobileNo;
        }
    }
}