using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.SuperAdmin.Model;
using CRM.SuperAdmin.View.AddAdminCustomer;
using CRM.SuperAdmin.View.AdminCustomer;
using CRM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.SuperAdmin.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        #region CTOR
        public HomeViewModel(INavigation navigation) : base(navigation)
        {
        }
        #endregion

        #region Commands
        public DelegateCommand LeadsCalledCommand => new DelegateCommand(ExecuteOnLeadsCalled);
        public DelegateCommand AddAdminCustomerCommand => new DelegateCommand(ExecuteOnAddAdminCustomer);
        #endregion

        #region Properties
        private HomeData _homeData = new HomeData();
        public HomeData HomeData
        {
            get => _homeData;
            set { _homeData = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async Task Initialize()
        {
            try
            {
                ShowLoading();
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    HttpClientHelper apicall = new HttpClientHelper(ApiUrls.GetDashboardDataForAdminUrl, string.Empty);
                    var response = await apicall.Get<HomeModel>();
                    if (response != null && response.Result && response.Object != null)
                    {
                        HomeData = response.Object;
                        HideLoading();
                    }
                    else
                    {
                        HideLoading();
                        await ShowAlert(response.Message);
                    }
                }
                else
                {
                    HideLoading();
                    await UserDialogs.Instance.AlertAsync(AppConstant.NETWORK_FAILURE, "", "Ok");
                }
            }
            catch (Exception ex)
            {
                await ShowAlert(ex.Message);
                HideLoading();
            }
        }
        public void ExecuteOnLeadsCalled(object obj)
        {
            App.MasterDetailPage.Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(AdminCustomerPage)))
            {
                BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                BarTextColor = Color.FromHex(App.nav_bar_text_color)
            };
        }
        public void ExecuteOnAddAdminCustomer(object obj)
        {
            App.MasterDetailPage.Detail = new NavigationPage(new AddAdminCustomerPage(null))
            {
                BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                BarTextColor = Color.FromHex(App.nav_bar_text_color)
            };
        }
        #endregion
    }
}
