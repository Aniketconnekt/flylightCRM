using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.SuperAdmin.Model;
using CRM.SuperAdmin.View.Successful;
using CRM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.SuperAdmin.ViewModel
{
    public class UsersViewModel : BaseViewModel
    {
        #region Data Members
        AdminCustomerData _adminCustomerData;
        #endregion

        #region CTOR
        public UsersViewModel(INavigation navigation) : base(navigation)
        {
        }
        #endregion

        #region Commands
        public DelegateCommand DownloadUserDetailsCommand => new DelegateCommand(ExecuteOnDownloadUserDetails);
        public DelegateCommand LoadMoreItemsCommand => new DelegateCommand(ExecuteOnLoadMoreItems);
        #endregion

        #region Properties
        private string _userName;
        public string UserName
        {
            get => _userName;
            set { _userName = value; OnPropertyChanged(); }
        }

        private ObservableCollection<UsersData> _usersData;
        public ObservableCollection<UsersData> UsersData
        {
            get => _usersData;
            set { _usersData = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async void Initialize(AdminCustomerData adminCustomerData)
        {
            _adminCustomerData = adminCustomerData;
            UserName = adminCustomerData.UserName;
            UsersData = new ObservableCollection<UsersData>();
            await GetUsersData(adminCustomerData, 0);
            if (!UsersData.Any())
                await ShowAlert("Record Not Found");
        }
        private async void ExecuteOnLoadMoreItems(object obj)
        {
            if (UsersData.Any())
                await GetUsersData(_adminCustomerData, UsersData.LastOrDefault().Id);
        }
        private async Task GetUsersData(AdminCustomerData adminCustomerData, int lastrecordsid)
        {
            try
            {
                ShowLoading();
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.GetUserListByAdminIdUrl, adminCustomerData.Id, lastrecordsid), string.Empty);
                    var response = await apicall.Get<UsersModel>();
                    if (response != null && response.Result && response.List != null && response.Message.Equals("Success"))
                    {
                        foreach (var users in response.List)
                            UsersData.Add(users);

                        if (response.List.Count() < 20 || response.List.Count() == response.TotalCount)
                            LoadMoreOption = Syncfusion.ListView.XForms.LoadMoreOption.None;
                        else
                            LoadMoreOption = Syncfusion.ListView.XForms.LoadMoreOption.Manual;
                    }
                    else
                        LoadMoreOption = Syncfusion.ListView.XForms.LoadMoreOption.None;
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
            HideLoading();
        }
        public async void ExecuteOnDownloadUserDetails(object obj)
        {
            Uri uri = new Uri(String.Format("http://flylight.connekt.in///api/Service/DownloadUserList?AdminId={0}", _adminCustomerData.Id));
            await Browser.OpenAsync(uri);
            await Task.Delay(100);
            await _navigation.PushAsync(new SuccessfulPage());
        }
        #endregion
    }
}