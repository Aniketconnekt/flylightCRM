using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model.AdminModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.ViewModel.AdminViewModel
{
    public class UserLeadsCallDetailsViewModel : BaseViewModel
    {
        #region CTOR
        public UserLeadsCallDetailsViewModel(INavigation navigation) : base(navigation)
        {
        }
        #endregion

        #region Command
        public DelegateCommand SearchCommand => new DelegateCommand(ExecuteOnSearch);
        public DelegateCommand LeadsSearch => new DelegateCommand(ExecuteOnLeadsSearch);
        public DelegateCommand LoadMoreItemsCommand => new DelegateCommand(ExecuteOnLoadMoreItems);
        #endregion

        #region Properties
        private List<CampaignData> _campaignList;
        public List<CampaignData> CampaignList
        {
            get => _campaignList;
            set { _campaignList = value; OnPropertyChanged(); }
        }

        private CampaignData _selectedCampaign;
        public CampaignData SelectedCampaign
        {
            get => _selectedCampaign;
            set { _selectedCampaign = value; OnPropertyChanged(); }
        }

        private ObservableCollection<UserLeadsCallDetailsData> _userLeadsCallDetailsData;
        public ObservableCollection<UserLeadsCallDetailsData> UserLeadsCallDetailsData
        {
            get => _userLeadsCallDetailsData;
            set { _userLeadsCallDetailsData = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async Task Initialize()
        {
            ShowLoading();
            var data = await Task.Run(async () => await GetCampaignData());
            CampaignList = data.ToList();
        }
        public async void ExecuteOnSearch(object obj)
        {
            if (SelectedCampaign != null)
            {
                UserLeadsCallDetailsData = new ObservableCollection<UserLeadsCallDetailsData>();
                await GetUserLeadsCallDetailsData(0);
                if (!UserLeadsCallDetailsData.Any())
                    await ShowAlert("Record Not Found");
            }
        }
        private async void ExecuteOnLoadMoreItems(object obj)
        {
            if (UserLeadsCallDetailsData.Any())
                await GetUserLeadsCallDetailsData(UserLeadsCallDetailsData.LastOrDefault().Id);
        }
        public async Task GetUserLeadsCallDetailsData(int lastrecordsid)
        {
            try
            {
                ShowLoading();
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    var userId = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.GetLeadsDetailListForAdminUrl, userId, SelectedCampaign.Id, lastrecordsid), string.Empty);
                    var response = await apicall.Get<UserLeadsCallDetailsModel>();
                    if (response != null && response.Result && response.List != null && response.Message.Equals("Success"))
                    {
                        foreach (var lead in response.List)
                            UserLeadsCallDetailsData.Add(lead);

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
        public async void ExecuteOnLeadsSearch(object obj)
        {
            UserLeadsCallDetailsData = new ObservableCollection<UserLeadsCallDetailsData>();
            UserLeadsCallDetailsData = await GetSearchLeadsData(obj as string);
        }
        public async Task<ObservableCollection<UserLeadsCallDetailsData>> GetSearchLeadsData(string searchValue)
        {
            var list = new ObservableCollection<UserLeadsCallDetailsData>();
            try
            {
                ShowLoading();
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    var userId = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.SearchLeadsDetailForAdminUrl, userId,SelectedCampaign.Id, searchValue), string.Empty);
                    var response = await apicall.Get<UserLeadsCallDetailsModel>();
                    if (response != null && response.Result && response.List != null && response.Message.Equals("Success"))
                    {
                        foreach (var leads in response.List)
                            list.Add(leads);
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
        #endregion
    }
}
