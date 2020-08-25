using System;
using CRM.Model;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using Acr.UserDialogs;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using System.Collections.ObjectModel;
using CRM.Common.Command;
using CRM.View.SearchLead;
using System.Linq;

namespace CRM.ViewModel
{
    public class LeadViewModel : BaseViewModel
    {
        #region CTOR
        public LeadViewModel(INavigation navigation) : base(navigation)
        {
        }
        #endregion

        #region Commands
        public DelegateCommand SearchLeadCommand => new DelegateCommand(ExecuteOnSearchLead);
        public DelegateCommand FollowupCommand => new DelegateCommand(ExecuteOnFollowup);
        public DelegateCommand LoadMoreItemsCommand => new DelegateCommand(ExecuteOnLoadMoreItems);
        #endregion

        #region Properties
        private NewLeadData _newLeadData = new NewLeadData();
        public NewLeadData NewLeadData
        {
            get => _newLeadData;
            set { _newLeadData = value; OnPropertyChanged(); }
        }

        private ObservableCollection<NewLeadsData> _leadsData;
        public ObservableCollection<NewLeadsData> LeadsData
        {
            get => _leadsData;
            set { _leadsData = value; OnPropertyChanged(); }
        }

        private string _leadCount;
        public string LeadCount
        {
            get => _leadCount;
            set { _leadCount = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async Task Initialize()
        {
            await Task.Run(async () => await GetDashboardData());

            LeadsData = new ObservableCollection<NewLeadsData>();
            await GetLeadsData(0);

            if (!LeadsData.Any())
                await ShowAlert("Record Not Found");
        }
        private async void ExecuteOnLoadMoreItems(object obj)
        {
            if (LeadsData.Any())
                await GetLeadsData(LeadsData.LastOrDefault().Id);
        }
        private async Task GetDashboardData()
        {
            try
            {
                ShowLoading();
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    var userId = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.GetDashboardDataUrl,userId), string.Empty);
                    var response = await apicall.Get<NewLeadModel>();
                    if (response != null && response.Result && response.Object != null && response.Message.Equals("Success"))
                        NewLeadData = response.Object;
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
        public async Task GetLeadsData(int lastrecordsid)
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    var userId = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.GetLeadsUrl, userId, lastrecordsid), string.Empty);
                    var response = await apicall.Get<Leads>();
                    if (response != null && response.Result && response.List != null && response.Message.Equals("Success"))
                    {
                        foreach (var lead in response.List)
                            LeadsData.Add(lead);

                        LeadCount = "Lead: " + LeadsData.Count() + "/" + response.TotalCount;

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
        public async void ExecuteOnSearchLead(object obj)
        {
            await _navigation.PushAsync(new SearchLeadPage());
        }
        public async void ExecuteOnFollowup(object obj)
        {
            try
            {
                App.MasterDetailPage.Detail = new NavigationPage(new View.FollowUp.FollowUpPage())
                {
                    BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                    BarTextColor = Color.FromHex(App.nav_bar_text_color),
                };
            }
            catch (Exception ex)
            {
                await ShowAlert(ex.Message);
            }
        }
        #endregion
    }
}