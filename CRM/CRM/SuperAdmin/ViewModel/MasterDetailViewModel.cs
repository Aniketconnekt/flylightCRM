using CRM.Common.Helpers;
using CRM.SuperAdmin.Model;
using CRM.ViewModel;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CRM.SuperAdmin.ViewModel
{
    public class MasterDetailViewModel : BaseViewModel
    {
        #region CTOR
        public MasterDetailViewModel(INavigation navigation) : base(navigation)
        {
            try
            {
                LoginUserName = Settings.CRM_LoginUserName;//SecureStorage.GetAsync(AppConstant.LoginUserName).Result;
                list = new List<MasterDetailModel>
                {
                    new MasterDetailModel(_navigation)
                    {
                        id=1,
                        icon="ic_address_active.png",
                        title ="Home",
                        title_text_color ="#2BAAE1",
                        row_background_color="#fafafa"
                    },
                    new MasterDetailModel(_navigation)
                    {
                        id=2,
                        icon="ic_menu_inactive_add_user.png",
                        title ="Add Admin/Customer",
                        title_text_color ="#4C4C4C",
                        row_background_color="#ffffff"
                    },
                    new MasterDetailModel(_navigation)
                    {
                        id=3,
                        icon="ic_menu_inactive_performance.png",
                        title ="Performance",
                        title_text_color="#4C4C4C",
                        row_background_color="#ffffff"
                    },
                    new MasterDetailModel(_navigation)
                    {
                        id=4,
                        icon="ic_share_inactive.png",
                        title ="Share App",
                        title_text_color="#4C4C4C",
                        row_background_color ="#ffffff"
                    },
                    new MasterDetailModel(_navigation)
                    {
                        id=5,
                        icon="ic_call_inactive.png",
                        title ="Contact Us",
                        title_text_color="#4C4C4C",
                        row_background_color ="#ffffff"
                    },
                    new MasterDetailModel(_navigation)
                    {
                        id=6,
                        icon="ic_menu_inactive_logout.png",
                        title ="Logout",
                        title_text_color="#4C4C4C",
                        row_background_color ="#ffffff"
                    }
                };
            }
            catch (Exception ex)
            {
            }
        } 
        #endregion

        #region Properties
        private List<MasterDetailModel> _list;
        public List<MasterDetailModel> list
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged("list");
            }
        }

        private string _loginUserName;
        public string LoginUserName
        {
            get => _loginUserName;
            set { _loginUserName = value; OnPropertyChanged(); }
        }
        #endregion
    }
}