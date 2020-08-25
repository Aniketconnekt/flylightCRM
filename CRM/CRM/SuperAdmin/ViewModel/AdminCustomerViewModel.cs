using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.SuperAdmin.Model;
using CRM.SuperAdmin.View.AddAdminCustomer;
using CRM.SuperAdmin.View.Performance;
using CRM.SuperAdmin.View.Successful;
using CRM.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.SuperAdmin.ViewModel
{
    public class AdminCustomerViewModel : BaseViewModel
    {
        #region CTOR
        public AdminCustomerViewModel(INavigation navigation) : base(navigation)
        {
        }
        #endregion

        #region Commands
        //public DelegateCommand DeleteCommand => new DelegateCommand(ExecuteOnDelete);
        public DelegateCommand EditCommand => new DelegateCommand(ExecuteOnEdit);
        public DelegateCommand AddAdminCustomerCommand => new DelegateCommand(ExecuteOnAddAdminCustomer);
        public DelegateCommand DownloadAdminDetailsCommand => new DelegateCommand(ExecuteOnDownloadAdminDetails);
        public DelegateCommand MoreDetailsCommand => new DelegateCommand(ExecuteOnMoreDetails);
        public DelegateCommand AdminCustomerSearch => new DelegateCommand(ExecuteOnAdminCustomerSearch);
        #endregion

        #region Properties
        private ObservableCollection<AdminCustomerData> _adminCustomerData;
        public ObservableCollection<AdminCustomerData> AdminCustomerData
        {
            get => _adminCustomerData;
            set { _adminCustomerData = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async Task Initialize()
        {
            AdminCustomerData = await GetAdminCustomerData();
        }
        private async Task<ObservableCollection<AdminCustomerData>> GetAdminCustomerData()
        {
            var list = new ObservableCollection<AdminCustomerData>();
            try
            {
                ShowLoading();
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    HttpClientHelper apicall = new HttpClientHelper(ApiUrls.GetAdminUserListUrl, string.Empty);
                    var response = await apicall.Get<AdminCustomerModel>();
                    if (response != null && response.Result && response.List != null)
                    {
                        foreach(var adminCustomer in response.List)
                            list.Add(adminCustomer);

                        var data = new ObservableCollection<AdminCustomerData>(list.OrderByDescending(o => o.CreationDate));
                        list = data;
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
            HideLoading();
            return list;
        }
        //public async void ExecuteOnDelete(object obj)
        //{
        //    var result = await UserDialogs.Instance.ConfirmAsync("Do you want to delete?", "", "Yes", "No");
        //    if (result)
        //    {
        //        ShowLoading();
        //        var user = obj as AdminCustomerData;
        //        var current = Connectivity.NetworkAccess;
        //        if (current == NetworkAccess.Internet)
        //        {
        //            HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.DeleteAdminUserUrl, user.Id), string.Empty);
        //            var response = await apicall.Get<bool>();
        //            if (response)
        //            {
        //                HideLoading();
        //                await ShowAlert("Admin/Customer deleted successfully.");
        //                await Initialize();
        //            }
        //            else
        //            {
        //                HideLoading();
        //                await ShowAlert(AppConstant.SOMETHING_WRONG);
        //            }
        //        }
        //        else
        //        {
        //            HideLoading();
        //            await UserDialogs.Instance.AlertAsync(AppConstant.NETWORK_FAILURE, "", "Ok");
        //        }
        //    }
        //}
        public async void ExecuteOnEdit(object obj)
        {
            await _navigation.PushAsync(new AddAdminCustomerPage(obj as AdminCustomerData));
        }
        public async void ExecuteOnAddAdminCustomer(object obj)
        {
            await _navigation.PushAsync(new AddAdminCustomerPage(null));
        }
        public async void ExecuteOnDownloadAdminDetails(object obj)
        {
            Uri uri = new Uri("http://flylight.connekt.in///api/Service/DownloadAdmins");
            await Browser.OpenAsync(uri);
            await Task.Delay(100);
            await _navigation.PushAsync(new SuccessfulPage());
        }
        public async void ExecuteOnMoreDetails(object obj)
        {
            await _navigation.PushAsync(new MoreDetailPage(obj as AdminCustomerData));
        }
        public async void ExecuteOnAdminCustomerSearch(object obj)
        {
            AdminCustomerData = await GetSearchAdminCustomerData(obj as string);
        }
        private async Task<ObservableCollection<AdminCustomerData>> GetSearchAdminCustomerData(string searchValue)
        {
            var list = new ObservableCollection<AdminCustomerData>();
            try
            {
                ShowLoading();
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.SearchAdminUserListUrl, searchValue), string.Empty);
                    var response = await apicall.Get<AdminCustomerModel>();
                    if (response != null && response.Result && response.List != null)
                    {
                        foreach (var adminCustomer in response.List)
                            list.Add(adminCustomer);

                        var data = new ObservableCollection<AdminCustomerData>(list.OrderByDescending(o => o.CreationDate));
                        list = data;
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
            HideLoading();
            return list;
        }
        #endregion
    }
}