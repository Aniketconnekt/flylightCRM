using System;
using System.Collections.Generic;
using CRM.Model;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;
using CRM.Common.Constants;
using CRM.Common.Helpers;

namespace CRM.ViewModel
{
    public class MasterDetailViewModel : BaseViewModel
    {
        #region Properties

        private ObservableCollection<MasterDetailModel> _list;
        public ObservableCollection<MasterDetailModel> list
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

        #region Constructor
        public MasterDetailViewModel(INavigation navigation) : base(navigation)
        {
            try
            {
                var roleId = Settings.CRM_UserRole; //SecureStorage.GetAsync(AppConstant.UserRole);
                LoginUserName = Settings.CRM_LoginUserName; //SecureStorage.GetAsync(AppConstant.LoginUserName).Result;
                switch (roleId)
                {
                    case "1":
                        GetUserMenuList();
                        break;
                    case "2":
                        GetAdminMenuList();
                        break;
                    case "3":
                        GetUserMenuList();
                        break;
                    case "4":
                        GetUserMenuList();
                        break;
                    default:
                        GetUserMenuList();
                        break;
                }
            }
            catch (Exception ex)
            {
                //
            }
        }
        #endregion

        #region Private
        private void GetAdminMenuList()
        {
            try
            {
                list = new ObservableCollection<MasterDetailModel>
                {
                    new MasterDetailModel(_navigation)
                    {
                        id=1,
                        icon="ic_menu_history.png",
                        title ="Dashboard",
                        title_text_color = App.nav_bar_color,
                        seperator_visible = true,
                        seperator_color = App.nav_bar_color,
                        roll = "Admin"
                    },
                    new MasterDetailModel(_navigation)
                    {
                        id=2,
                        icon="ic_menu_inactive_campaign.png",
                        title ="Campaigns",
                        title_text_color ="#4C4C4C",
                        seperator_visible = false,
                        seperator_color = App.nav_bar_color,
                        roll = "Admin"
                    },
                    new MasterDetailModel(_navigation)
                    {
                        id=3,
                        icon="ic_menu_inactive_add_user.png",
                        title ="Add Users",
                        title_text_color="#4C4C4C",
                        seperator_visible = false,
                        seperator_color = App.nav_bar_color,
                        roll = "Admin"
                    },
                    new MasterDetailModel(_navigation)
                    {
                        id=4,
                        icon="ic_menu_inactive_add_lead.png",
                        title ="Leads",
                        title_text_color="#4C4C4C",
                        seperator_visible = false,
                        seperator_color = App.nav_bar_color,
                        roll = "Admin"
                    },
                    new MasterDetailModel(_navigation)
                    {
                        id=5,
                        icon="ic_menu_inactive_leadallotment.png",
                        title ="Lead Allottment",
                        title_text_color="#4C4C4C",
                        seperator_visible = false,
                        seperator_color = App.nav_bar_color,
                        roll = "Admin"
                    },
                     new MasterDetailModel(_navigation)
                    {
                        id=6,
                        icon="ic_transfer_inactive.png",
                        title ="Transfer Leads",
                        title_text_color="#4C4C4C",
                        seperator_visible = false,
                        seperator_color = App.nav_bar_color,
                        roll = "Admin"
                    },
                    new MasterDetailModel(_navigation)
                    {
                        id=7,
                        icon="ic_menu_inactive_performance.png",
                        title ="Performance",
                        title_text_color="#4C4C4C",
                        seperator_visible = false,
                        seperator_color = App.nav_bar_color,
                        roll = "Admin"
                    },
                    new MasterDetailModel(_navigation)
                    {
                        id=8,
                        icon="ic_addstatus.png",
                        title ="Add Status",
                        title_text_color ="#4C4C4C",
                        seperator_visible = false,
                        seperator_color = App.nav_bar_color,
                        roll = "Admin"
                    },
                    new MasterDetailModel(_navigation)
                    {
                        id=9,
                        icon="ic_menu_inactive_language.png",
                        title ="Language Settings",
                        title_text_color="#4C4C4C",
                        seperator_visible = false,
                        seperator_color = App.nav_bar_color,
                        roll = "Admin"
                    },
                    new MasterDetailModel(_navigation)
                    {
                        id=10,
                        icon="ic_share_inactive.png",
                        title ="Share App",
                        title_text_color="#4C4C4C",
                        seperator_visible = false,
                        seperator_color = App.nav_bar_color,
                        roll = "Admin"
                    },
                    new MasterDetailModel(_navigation)
                    {
                        id=11,
                        icon="ic_call_inactive.png",
                        title ="Contact Us",
                        title_text_color="#4C4C4C",
                        seperator_visible = false,
                        seperator_color = App.nav_bar_color,
                        roll = "Admin"
                    },
                    new MasterDetailModel(_navigation)
                    {
                        id=12,
                        icon="ic_menu_inactive_logout.png",
                        title ="Logout",
                        title_text_color="#4C4C4C",
                        seperator_visible = false,
                        seperator_color = App.nav_bar_color,
                        roll = "Admin"
                    }
                };
            }
            catch (Exception ex)
            {
                //
            }
        }
        private void GetUserMenuList()
        {
            try
            {
                list = new ObservableCollection<MasterDetailModel>
                {
                    new MasterDetailModel(_navigation)
                    {
                        id=1,
                        icon="ic_menu_add_lead.png",
                        title ="Dashboard",
                        title_text_color = App.nav_bar_color,
                        seperator_visible = true,
                        seperator_color = App.nav_bar_color,
                        roll = "User",
                    },
                    new MasterDetailModel(_navigation)
                    {
                        id=2,
                        icon="ic_menu_inactive_add_lead.png",
                        title ="Leads",
                        title_text_color ="#4C4C4C",
                        seperator_visible = false,
                        seperator_color = App.nav_bar_color,
                        roll = "User",
                    },
                    new MasterDetailModel(_navigation)
                    {
                        id=3,
                        icon="ic_menu_inactive_allotted_lead.png",
                        title ="Allotted Leads",
                        title_text_color="#4C4C4C",
                        seperator_visible = false,
                        seperator_color = App.nav_bar_color,
                        roll = "User",
                    },
                    new MasterDetailModel(_navigation)
                    {
                        id=4,
                        icon="ic_menu_inactive_followup.png",
                        title ="Follow UP",
                        title_text_color="#4C4C4C",
                        seperator_visible = false,
                          seperator_color = App.nav_bar_color,
                        roll = "User",
                    },
                    new MasterDetailModel(_navigation)
                    {
                        id=5,
                        icon="ic_menu_inactive_performance.png",
                        title ="Performance",
                        title_text_color="#4C4C4C",
                        seperator_visible = false,
                          seperator_color = App.nav_bar_color,
                        roll = "User",
                    },
                    new MasterDetailModel(_navigation)
                    {
                        id=6,
                        icon="ic_menu_inactive_history.png",
                        title ="Lead History",
                        title_text_color="#4C4C4C",
                        seperator_visible = false,
                          seperator_color = App.nav_bar_color,
                        roll = "User"
                    },
                     new MasterDetailModel(_navigation)
                     {
                        id = 7,
                        icon = "ic_email.png",
                        title = "Send Message",
                        title_text_color = "#4C4C4C",
                        seperator_visible = false,
                        seperator_color = App.nav_bar_color,
                        roll = "User"
                    },
                    new MasterDetailModel(_navigation)
                    {
                        id=8,
                        icon="ic_menu_inactive_language.png",
                        title ="Language Settings",
                        title_text_color="#4C4C4C",
                        seperator_visible = false,
                        seperator_color = App.nav_bar_color,
                        roll = "User"
                    },
                    new MasterDetailModel(_navigation)
                    {
                        id=9,
                        icon="ic_share_inactive.png",
                        title ="Share App",
                        title_text_color="#4C4C4C",
                        seperator_visible = false,
                        seperator_color = App.nav_bar_color,
                        roll = "User"
                    },
                    new MasterDetailModel(_navigation)
                    {
                        id=10,
                        icon="ic_call_inactive.png",
                        title ="Contact Us",
                        title_text_color="#4C4C4C",
                        seperator_visible = false,
                        seperator_color = App.nav_bar_color,
                        roll = "User"
                    },
                    new MasterDetailModel(_navigation)
                    {
                        id=11,
                        icon="ic_menu_inactive_logout.png",
                        title ="Logout",
                        title_text_color="#4C4C4C",
                        seperator_visible = false,
                        seperator_color = App.nav_bar_color,
                        roll = "User"
                    }
                };
            }
            catch (Exception ex)
            {
                //
            }
        }
        #endregion
    }
}