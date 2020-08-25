using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model;
using CRM.Model.AdminModel;
using CRM.View.LeadAllotted;
using CRM.View.LeadCalled;
using CRM.View.LeadsPending;
using CRM.View.LeadUnCalled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.ViewModel
{
    public class PerformanceViewModel : BaseViewModel
    {
        #region CTOR
        public PerformanceViewModel(INavigation navigation) : base(navigation)
        {
        }
        #endregion

        #region Commands
        public DelegateCommand SearchCommand => new DelegateCommand(ExecuteOnSearch);
        public DelegateCommand LeadsAllottedCommand => new DelegateCommand(ExecuteOnLeadsAllotted);
        public DelegateCommand LeadsCalledCommand => new DelegateCommand(ExecuteOnLeadsCalled);
        public DelegateCommand LeadsPendingCommand => new DelegateCommand(ExecuteOnLeadsPending);
        public DelegateCommand LeadsUnCalledCommand => new DelegateCommand(ExecuteOnLeadsUnCalled);
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

        private PerformanceData _performanceData = new PerformanceData();
        public PerformanceData PerformanceData
        {
            get => _performanceData;
            set { _performanceData = value; OnPropertyChanged(); }
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
                ShowLoading();
                await GetPerformanceData();
                IsAnotherPartVisible = true;
                MessagingCenter.Send<PerformanceViewModel>(this, "UserModelFilled");
            }
            else
                await ShowAlert("Please select campaign first.");
        }
        public async Task GetPerformanceData()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    var userId = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.GetUserPerformanceUrl, userId, SelectedCampaign.Id, 0), string.Empty);
                    var response = await apicall.Get<PerformanceModel>();
                    if (response != null && response.Result && response.Object != null && response.Message.Equals("Success"))
                    {
                        PerformanceData = response.Object;
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
                HideLoading();
                await ShowAlert(ex.Message);
            }
        }
        public async void ExecuteOnLeadsAllotted(object obj)
        {
            if (PerformanceData.TotalLeads != 0)
                await _navigation.PushAsync(new LeadAllottedPage(SelectedCampaign));
        }
        public async void ExecuteOnLeadsCalled(object obj)
        {
            if (PerformanceData.LeadsCalled != 0)
                await _navigation.PushAsync(new LeadCalledPage(SelectedCampaign, "Called"));
        }
        public async void ExecuteOnLeadsPending(object obj)
        {
            if (PerformanceData.Leadspending != 0)
                await _navigation.PushAsync(new LeadsPendingPage(SelectedCampaign, "Pending"));
        }
        public async void ExecuteOnLeadsUnCalled(object obj)
        {
            if (PerformanceData.LeadsUnCalled != 0)
                await _navigation.PushAsync(new LeadUnCalledPage(SelectedCampaign, "UnCalled"));
        }
        #endregion
    }
}