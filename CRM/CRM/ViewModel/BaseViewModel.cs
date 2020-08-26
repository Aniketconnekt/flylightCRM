using Acr.UserDialogs;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model;
using CRM.Model.AdminModel;
using CRM.SuperAdmin.Model;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected bool SetProperty<T>(
          ref T backingStore, T value,
          [CallerMemberName] string propertyName = "",
          Action onChanged = null)
        {


            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;

            if (onChanged != null)
                onChanged();

            OnPropertyChanged(propertyName);
            return true;
        }

        public INavigation _navigation;

        #region CTOR
        public BaseViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }
        #endregion

        #region Properties
        private LoadMoreOption _loadMoreOption = LoadMoreOption.None;
        public LoadMoreOption LoadMoreOption
        {
            get => _loadMoreOption;
            set { _loadMoreOption = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async Task ShowAlert(string message)
        {
            await UserDialogs.Instance.AlertAsync(message);
        }
        public void ShowLoading(string message = "Loading")
        {
            UserDialogs.Instance.ShowLoading(message, MaskType.Gradient);
        }
        public void HideLoading()
        {
            UserDialogs.Instance.HideLoading();
        }
        public async void ExecuteOnBack()
        {
            await _navigation.PopAsync();
        }

        public async Task<ObservableCollection<CampaignData>> GetCampaignData()
        {
            var list = new ObservableCollection<CampaignData>();
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    var userId = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.GetcampdetailbyuseridUrl, userId), string.Empty);
                    var response = await apicall.Get<CampaignModel>();
                    if (response != null && response.Result && response.List != null && response.Message.Equals("Success"))
                    {
                        foreach (var users in response.List)
                            list.Add(users);
                    }
                    else
                    {
                        HideLoading();
                        await ShowAlert("Record Not Found");
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
            HideLoading();
            return list;
        }
        public async Task<ObservableCollection<UsersData>> GetUsersData()
        {
            var list = new ObservableCollection<UsersData>();
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    var userId = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.GetUserListByAdminIdUrl, userId, 0), string.Empty);
                    var response = await apicall.Get<UsersModel>();
                    if (response != null && response.Result && response.List != null && response.Message.Equals("Success"))
                    {
                        foreach (var users in response.List)
                            list.Add(users);
                    }
                    else
                    {
                        HideLoading();
                        await ShowAlert("Record Not Found");
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
        public async Task<ObservableCollection<NewCampaignData>> GetCampaignList()
        {
            var list = new ObservableCollection<NewCampaignData>();
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    var createdById = Settings.CRM_CreatedById; //await SecureStorage.GetAsync(AppConstant.CreatedById);
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.GetCampaignListUrl, createdById), string.Empty);
                    var response = await apicall.Get<NewCampaignModel>();
                    if (response != null && response.Result && response.List != null && response.Message.Equals("Success"))
                    {
                        foreach (var camp in response.List)
                            list.Add(camp);
                    }
                    else
                    {
                        HideLoading();
                        await ShowAlert("Record Not Found");
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
            HideLoading();
            return list;
        }
        public async Task<ObservableCollection<CampaignActionData>> GetLadActionList()
        {
            var list = new ObservableCollection<CampaignActionData>();
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    var userId = Settings.CRM_CreatedById; //await SecureStorage.GetAsync(AppConstant.CreatedById);
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.GetLadAction, userId), string.Empty);
                    var response = await apicall.Get<CampaignAction>();
                    if (response != null && response.Result && response.List != null && response.Message.Equals("Success"))
                    {
                        foreach (var status in response.List)
                            list.Add(status);
                    }
                    else
                    {
                        HideLoading();
                        await ShowAlert("Record Not Found");
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

        public async Task<ObservableCollection<StateData>> GetStateList()
        {
            var list = new ObservableCollection<StateData>();
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.GetStateListUrl, 1), string.Empty);
                    var response = await apicall.Get<StateModel>();
                    if (response != null && response.Result && response.List != null && response.Message.Equals("Success"))
                    {
                        foreach (var state in response.List)
                            list.Add(state);
                    }
                    else
                    {
                        HideLoading();
                        await ShowAlert("Record Not Found");
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
            HideLoading();
            return list;
        }

        public ObservableCollection<object> InitializeDate()
        {
            ObservableCollection<object> todaycollection = new ObservableCollection<object>();
            if (DateTime.Now.Date.Day < 10)
                todaycollection.Add("0" + DateTime.Now.Date.Day);
            else
                todaycollection.Add(DateTime.Now.Date.Day.ToString());

            if (DateTime.Now.Date.Month < 10)
                todaycollection.Add("0" + DateTime.Now.Date.Month);
            else
                todaycollection.Add(DateTime.Now.Date.Month.ToString());

            todaycollection.Add(DateTime.Now.Date.Year.ToString());

            return todaycollection;
        }
        #endregion

        #region INotifyPropertyChanged Implemented
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}