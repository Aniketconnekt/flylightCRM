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
    public class LeadCalledViewModel : BaseViewModel
    {
        #region Data Members
        private string _type = String.Empty;
        int _totalCount = 0;
        #endregion

        #region CTOR
        public LeadCalledViewModel(INavigation navigation) : base(navigation)
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

        private bool _isAnotherPartVisible;
        public bool IsAnotherPartVisible
        {
            get => _isAnotherPartVisible;
            set { _isAnotherPartVisible = value; OnPropertyChanged(); }
        }

        private ObservableCollection<NewLeadsData> _leadCalledData;
        public ObservableCollection<NewLeadsData> LeadCalledData
        {
            get => _leadCalledData;
            set { _leadCalledData = value; OnPropertyChanged(); }
        }

        private string _leadsCount;
        public string LeadsCount
        {
            get => _leadsCount;
            set { _leadsCount = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async void Initialize(NewCampaignData selectedCampaign, string type)
        {
            ShowLoading();

            var data = await Task.Run(async () => await GetCampaignList());
            CampaignList = data.ToList();

            if (CampaignList.Any() && selectedCampaign != null)
            {
                SelectedCampaign = CampaignList.Find(c => c.Id == selectedCampaign.Id);
                ExecuteOnSearch(type);
            }

            IsAnotherPartVisible = false;
        }
        public async void ExecuteOnSearch(object obj)
        {
            _type = obj as string;
            if (SelectedCampaign != null)
            {
                LeadCalledData = new ObservableCollection<NewLeadsData>();
                await GetLeadCalledData(_type,0);
                if (!LeadCalledData.Any())
                {
                    await ShowAlert("Record Not Found");
                    _totalCount = 0;
                }

                LeadsCount = "Leads: " + LeadCalledData.Count() + "/" + _totalCount;
                IsAnotherPartVisible = true;
            }
            else
                await ShowAlert("Please select campaign first.");
        }
        private async void ExecuteOnLoadMoreItems(object obj)
        {
            if (LeadCalledData.Any())
                await GetLeadCalledData(_type,LeadCalledData.LastOrDefault().Id);

            LeadsCount = "Leads: " + LeadCalledData.Count() + "/" + _totalCount;
        }
        public async Task GetLeadCalledData(string type, int lastrecordsid)
        {
            try
            {
                ShowLoading();
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    var userID = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.GetLeadsListFromPerformanceUrl, userID, lastrecordsid, type, SelectedCampaign.Id), string.Empty);
                    var response = await apicall.Get<Leads>();
                    if (response != null && response.Result && response.List != null && response.Message.Equals("Success"))
                    {
                        foreach (var leadCall in response.List)
                            LeadCalledData.Add(leadCall);

                        _totalCount = response.TotalCount;

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