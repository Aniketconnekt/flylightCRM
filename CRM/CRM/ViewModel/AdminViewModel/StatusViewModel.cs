using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model.AdminModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.ViewModel.AdminViewModel
{
    public class StatusViewModel : BaseViewModel
    {
        #region CTOR
        public StatusViewModel(INavigation navigation) : base(navigation)
        {
        }
        #endregion

        #region Commands
        public DelegateCommand DeleteCommand => new DelegateCommand(ExecuteOnDelete);
        public DelegateCommand EditCommand => new DelegateCommand(ExecuteOnEdit);
        public DelegateCommand AddStatusCommand => new DelegateCommand(ExecuteOnAddStatus);
        #endregion

        #region Properties
        private ObservableCollection<StatusData> _statusData;
        public ObservableCollection<StatusData> StatusData
        {
            get => _statusData;
            set { _statusData = value; OnPropertyChanged(); }
        }

        public StatusModel _statusModel = new StatusModel();
        public StatusModel StatusModel
        {
            get => _statusModel;
            set { _statusModel = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async Task Initialize()
        {
            StatusData = await GetStatusData();
        }
        private async Task<ObservableCollection<StatusData>> GetStatusData()
        {
            var list = new ObservableCollection<StatusData>();
            try
            {
                ShowLoading();
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    var userId = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.GetstatusListUrl, userId), string.Empty);
                    var response = await apicall.Get<GetStatus>();
                    if (response != null && response.Result && response.List != null)
                    {
                        foreach (var status in response.List)
                            list.Add(status);

                        var data = new ObservableCollection<StatusData>(list.OrderByDescending(o => o.CreationDate));
                        list = data;
                    }
                    else
                    {
                        HideLoading();
                        await ShowAlert("Record Not Found.");
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
        public async void ExecuteOnAddStatus(object obj)
        {
            try
            {
                string result = await App.Current.MainPage.DisplayPromptAsync("", "Action Name", "Save", "Cancel", "Action Name", 30, Keyboard.Default, String.Empty);
                if (string.IsNullOrWhiteSpace(result))
                    return;

                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    ShowLoading();
                    HttpClientHelper apicall = new HttpClientHelper(ApiUrls.AddUpdateStatusUrl, string.Empty);
                    StatusModel.Id = 0;
                    StatusModel.ActionName = result;
                    StatusModel.CreatedById = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    var json = JsonConvert.SerializeObject(StatusModel);
                    var response = await apicall.Post<AddUpdateStatusResponse>(json);
                    if (response != null && response.Result && response.Object != null && response.Message.Equals("Success"))
                    {
                        HideLoading();
                        await ShowAlert("Status added successfully.");
                        StatusModel = new StatusModel();
                        await Initialize();
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
                HideLoading();
                await ShowAlert(ex.Message);
            }
        }
        public async void ExecuteOnDelete(object obj)
        {
            var result = await UserDialogs.Instance.ConfirmAsync("Do you want to delete?", "", "Yes", "No");
            if (result)
            {
                ShowLoading();
                var status = obj as StatusData;
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.DeleteStatusUrl, status.Id), string.Empty);
                    var response = await apicall.Get<bool>();
                    if (response)
                    {
                        HideLoading();
                        await ShowAlert("Status deleted successfully.");
                        await Initialize();
                    }
                    else
                    {
                        HideLoading();
                        await ShowAlert(AppConstant.SOMETHING_WRONG);
                    }
                }
                else
                {
                    HideLoading();
                    await UserDialogs.Instance.AlertAsync(AppConstant.NETWORK_FAILURE, "", "Ok");
                }
            }
        }
        public async void ExecuteOnEdit(object obj)
        {
            try
            {
                var status = obj as StatusData;
                string result = await App.Current.MainPage.DisplayPromptAsync("", "Action Name","Update","Cancel", "Action Name", 30, Keyboard.Default,status.ActionName);
                if (string.IsNullOrWhiteSpace(result))
                    return;

                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    ShowLoading();
                    HttpClientHelper apicall = new HttpClientHelper(ApiUrls.AddUpdateStatusUrl, string.Empty);
                    StatusModel.Id = status.Id;
                    StatusModel.ActionName = result;
                    StatusModel.CreatedById = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    var json = JsonConvert.SerializeObject(StatusModel);
                    var response = await apicall.Post<AddUpdateStatusResponse>(json);
                    if (response != null && response.Result && response.Object != null && response.Message.Equals("Success"))
                    {
                        HideLoading();
                        await ShowAlert("Status updated successfully.");
                        StatusModel = new StatusModel();
                        await Initialize();
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
                HideLoading();
                await ShowAlert(ex.Message);
            }
        }
        #endregion
    }
}