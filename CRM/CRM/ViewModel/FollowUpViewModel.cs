using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.ViewModel
{
    public class FollowUpViewModel : BaseViewModel
    {
        #region CTOR
        public FollowUpViewModel(INavigation navigation) : base(navigation)
        {
            EndDate = StartDate = InitializeDate();
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

        private string _fromDate = "dd-MM-yyyy";
        public string FromDate
        {
            get => _fromDate;
            set { _fromDate = value; OnPropertyChanged(); }
        }

        private string _toDate = "dd-MM-yyyy";
        public string ToDate
        {
            get => _toDate;
            set { _toDate = value; OnPropertyChanged(); }
        }

        private bool _isAnotherPartVisible;
        public bool IsAnotherPartVisible
        {
            get => _isAnotherPartVisible;
            set { _isAnotherPartVisible = value; OnPropertyChanged(); }
        }

        private ObservableCollection<NewLeadsData> _followUpData;
        public ObservableCollection<NewLeadsData> FollowUpData
        {
            get => _followUpData;
            set { _followUpData = value; OnPropertyChanged(); }
        }

        private string _leadsCount;
        public string LeadsCount
        {
            get => _leadsCount;
            set { _leadsCount = value; OnPropertyChanged(); }
        }

        private ObservableCollection<object> _startdate;
        public ObservableCollection<object> StartDate
        {
            get { return _startdate; }
            set
            {
                _startdate = value;
                OnPropertyChanged("StartDate");
                FromDate = _startdate.GetDateTime();
            }
        }

        private ObservableCollection<object> _endDate;
        public ObservableCollection<object> EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged("StartDate");
                ToDate = _endDate.GetDateTime();
            }
        }
        #endregion

        #region Methods
        public async void Initialize()
        {
            ShowLoading();

            var data = await Task.Run(async () => await GetCampaignList());
            CampaignList = data.ToList();

            IsAnotherPartVisible = false;
        }
        public async void ExecuteOnSearch(object obj)
        {
            if (SelectedCampaign != null)
            {
                FollowUpData = new ObservableCollection<NewLeadsData>();
                await GetFollowUpData(0);

                if (!FollowUpData.Any())
                    await ShowAlert("Record Not Found");
                
                IsAnotherPartVisible = true;
            }
            else
                await ShowAlert("Please select campaign first.");
        }
        private async void ExecuteOnLoadMoreItems(object obj)
        {
            if (FollowUpData.Any())
                await GetFollowUpData(FollowUpData.LastOrDefault().Id);
        }
        public async Task GetFollowUpData(int lastrecordsid)
        {
            try
            {
                ShowLoading();
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    var userid = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    string[] dobArr = FromDate.Split('-');
                    var fromDate = dobArr[1] + "-" + dobArr[0] + "-" + dobArr[2];
                    string[] toArr = ToDate.Split('-');
                    var toDate = toArr[1] + "-" + toArr[0] + "-" + toArr[2];
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.GetFollowupListForUserUrl, SelectedCampaign.Id, lastrecordsid, fromDate, toDate, userid), string.Empty);
                    var response = await apicall.Get<Leads>();
                    if (response != null && response.Result && response.List != null && response.Message.Equals("Success"))
                    {
                        foreach (var follow in response.List)
                            FollowUpData.Add(follow);

                        LeadsCount = "Leads: " + FollowUpData.Count() + "/" + response.TotalCount;

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
