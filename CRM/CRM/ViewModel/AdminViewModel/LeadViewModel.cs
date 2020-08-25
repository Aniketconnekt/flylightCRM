using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model.AdminModel;
using CRM.View.Admin.Compaign;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;
using CRM.View.Admin.Lead;
using CRM.View.Admin.FollowUp;

namespace CRM.ViewModel.AdminViewModel
{
    public class LeadViewModel : BaseViewModel
    {
        #region CTOR
        public LeadViewModel(INavigation navigation) : base(navigation)
        {
        }
        #endregion

        #region Commands
        public DelegateCommand ViewUsersCommand => new DelegateCommand(ExecuteOnViewUsers);
        public DelegateCommand ViewMoreCommand => new DelegateCommand(ExecuteOnViewMore);
        public DelegateCommand UserLeadsCallDetailsCommand => new DelegateCommand(ExecuteOnUserLeadsCallDetails);
        public DelegateCommand FollowUpCommand => new DelegateCommand(ExecuteOnFollowUp);
        public DelegateCommand LeadsByStatusCommand => new DelegateCommand(ExecuteOnLeadsByStatus);
        #endregion

        #region Properties
        private LeadData _leadData = new LeadData();
        public LeadData LeadData
        {
            get => _leadData;
            set { _leadData = value; OnPropertyChanged(); }
        }

        private ObservableCollection<CampaignData> _campaignData;
        public ObservableCollection<CampaignData> CampaignData
        {
            get => _campaignData;
            set { _campaignData = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async Task Initialize()
        {
            await Task.Run(() => GetDashboardData());
            var data = await Task.Run(async () => await GetCampaignData());
            var obj = new ObservableCollection<CampaignData>();
            if (data.Count() > 2)
            {
                for (int i = 0; i < 2; i++)
                    obj.Add(data[i]);

                CampaignData = obj;
            }
            else
                CampaignData = data;
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
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.GetDashboardDataForAdminUserUrl, userId), string.Empty);
                    var response = await apicall.Get<LeadModel>();
                    if (response != null && response.Result && response.Object != null && response.Message.Equals("Success"))
                        LeadData = response.Object;
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
        public void ExecuteOnViewUsers(object obj)
        {
            App.MasterDetailPage.Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(View.Admin.Users.Users)))
            {
                BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                BarTextColor = Color.FromHex(App.nav_bar_text_color)
            };
        }
        public void ExecuteOnViewMore(object obj)
        {
            App.MasterDetailPage.Detail = new NavigationPage(new Campaigns())
            {
                BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                BarTextColor = Color.FromHex(App.nav_bar_text_color),
            };
        }
        public async void ExecuteOnUserLeadsCallDetails(object obj)
        {
            await _navigation.PushAsync(new UserLeadsCallDetailsPage());
        }
        public async void ExecuteOnFollowUp(object obj)
        {
            await _navigation.PushAsync(new FollowUp());
        }
        public async void ExecuteOnLeadsByStatus(object obj)
        {
            await _navigation.PushAsync(new LeadsByStatusPage());
        }
        #endregion
    }
}
