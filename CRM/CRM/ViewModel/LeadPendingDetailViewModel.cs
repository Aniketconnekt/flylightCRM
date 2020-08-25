using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CRM.ViewModel
{
    public class LeadPendingDetailViewModel : BaseViewModel
    {
        public LeadPendingDetailViewModel(INavigation navigation) : base(navigation)
        {
            try
            {

                list = new List<LeadPendingDetailModel>
                {
                    new LeadPendingDetailModel
                    {
                        name="xyz",
                        mobile_number="0000000000",
                    },
                    new LeadPendingDetailModel
                    {
                        name="xyz",
                        mobile_number="0000000000",
                    },
                    new LeadPendingDetailModel
                    {
                        name="xyz",
                        mobile_number="0000000000",
                    },
                };
            }
            catch (Exception ex)
            {
            }
        }

        private List<LeadPendingDetailModel> _list;
        public List<LeadPendingDetailModel> list
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged("list");
            }
        }
    }
}