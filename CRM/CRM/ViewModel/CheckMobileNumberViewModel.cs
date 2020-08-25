using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CRM.ViewModel
{
    class CheckMobileNumberViewModel : BaseViewModel
    {
        long _mobileNo;
        public long MobileNo
        {
            get => _mobileNo;
            set
            {
                _mobileNo = value;
                OnPropertyChanged("MobileNo");
            }
        }

        public CheckMobileNumberViewModel(INavigation navigation) : base(navigation)
        {
        }
    }
}
