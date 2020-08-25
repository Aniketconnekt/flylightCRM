using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.ViewModel
{
    public class LeadHistoryViewModel : BaseViewModel
    {
        #region CTOR
        public LeadHistoryViewModel(INavigation navigation) : base(navigation)
        {
        }
        #endregion

        #region Commands
        public DelegateCommand SearchCommand => new DelegateCommand(ExecuteOnSearch);
        public DelegateCommand LoadMoreItemsCommand => new DelegateCommand(ExecuteOnLoadMoreItems);
        #endregion

        #region Properties
        private List<NewCampaignData> _campaignList;
        public List<NewCampaignData> CampaignList
        {
            get => _campaignList;
            set { _campaignList = value; OnPropertyChanged(); }
        }

        private NewCampaignData _selectedCampaign;
        public NewCampaignData SelectedCampaign
        {
            get => _selectedCampaign;
            set { _selectedCampaign = value; OnPropertyChanged(); }
        }

        private bool _isSearchBtnVisible;
        public bool IsSearchBtnVisible
        {
            get => _isSearchBtnVisible;
            set { _isSearchBtnVisible = value; OnPropertyChanged(); }
        }

        private bool _isAnotherPartVisible;
        public bool IsAnotherPartVisible
        {
            get => _isAnotherPartVisible;
            set { _isAnotherPartVisible = value; OnPropertyChanged(); }
        }

        private ObservableCollection<NewLeadsData> _leadHistoryData;
        public ObservableCollection<NewLeadsData> LeadHistoryData
        {
            get => _leadHistoryData;
            set { _leadHistoryData = value; OnPropertyChanged(); }
        }

        private string _leadsCount;
        public string LeadsCount
        {
            get => _leadsCount;
            set { _leadsCount = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async void Initialize()
        {
            ShowLoading();

            var data = await Task.Run(async () => await GetCampaignList());
            CampaignList = data.ToList();

            IsAnotherPartVisible = false;
            IsSearchBtnVisible = true;
        }
        
        public async void ExecuteOnSearch(object obj)
        {
            if (SelectedCampaign != null)
            {
                LeadHistoryData = new ObservableCollection<NewLeadsData>();
                await GetLeadHistoryData(0);
                if (!LeadHistoryData.Any())
                    await ShowAlert("Record Not Found");

                IsSearchBtnVisible = false;
                IsAnotherPartVisible = true;
            }
            else
                await ShowAlert("Please select campaign first.");
        }
        private async void ExecuteOnLoadMoreItems(object obj)
        {
            if (LeadHistoryData.Any())
                await GetLeadHistoryData(LeadHistoryData.LastOrDefault().Id);
        }
        public async Task GetLeadHistoryData(int lastrecordsid)
        {
            try
            {
                ShowLoading();
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    var userID = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.GetLeadsbycampidUrl,userID, SelectedCampaign.Id, lastrecordsid), string.Empty);
                    var response = await apicall.Get<Leads>();
                    if (response != null && response.Result && response.List != null && response.Message.Equals("Success"))
                    {
                        foreach (var history in response.List)
                            LeadHistoryData.Add(history);

                        LeadsCount = "Leads: " + LeadHistoryData.Count() + "/" + response.TotalCount;

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
                HideLoading();
                await ShowAlert(ex.Message);
            }
            HideLoading();
        }
        #endregion
    }
}
