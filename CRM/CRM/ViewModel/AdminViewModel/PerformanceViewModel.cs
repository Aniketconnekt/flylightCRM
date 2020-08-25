using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model.AdminModel;
using CRM.SuperAdmin.Model;
using CRM.View.Admin.Lead;
using CRM.View.Admin.Performance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.ViewModel.AdminViewModel
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
        public DelegateCommand Day1Command => new DelegateCommand(ExecuteOnDay1);
        public DelegateCommand Day7Command => new DelegateCommand(ExecuteOnDay7);
        public DelegateCommand Day30Command => new DelegateCommand(ExecuteOnDay30);
        public DelegateCommand ViewUserPerformanceCommand => new DelegateCommand(ExecuteOnViewUserPerformance);
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

        private List<UsersData> _usersList;
        public List<UsersData> UsersList
        {
            get => _usersList;
            set { _usersList = value; OnPropertyChanged(); }
        }

        private UsersData _selectedUsers;
        public UsersData SelectedUsers
        {
            get => _selectedUsers;
            set { _selectedUsers = value; OnPropertyChanged(); }
        }

        private bool _isViewPerformaceVisible = false;
        public bool IsViewPerformaceVisible
        {
            get => _isViewPerformaceVisible;
            set { _isViewPerformaceVisible = value; OnPropertyChanged(); }
        }

        private PerformanceData _performanceData = new PerformanceData();
        public PerformanceData PerformanceData
        {
            get => _performanceData;
            set { _performanceData = value; OnPropertyChanged(); }
        }

        private Color _day1Color = Color.FromHex("#4C4C4C");
        public Color Day1Color
        {
            get => _day1Color;
            set { _day1Color = value; OnPropertyChanged(); }
        }

        private Color _day7Color = Color.FromHex("#2baae1");
        public Color Day7Color
        {
            get => _day7Color;
            set { _day7Color = value; OnPropertyChanged(); }
        }

        private Color _day30Color = Color.FromHex("#4C4C4C");
        public Color Day30Color
        {
            get => _day30Color;
            set { _day30Color = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async void Initialize()
        {
            ShowLoading();
            var data = await Task.Run(async () => await GetCampaignData());
            CampaignList = data.ToList();
            var userData = await Task.Run(async () => await GetUsersData());
            UsersList = userData.ToList();
        }
        public async void ExecuteOnSearch(object obj)
        {
            if (await Validation())
            {
                try
                {
                    ShowLoading();
                    var current = Connectivity.NetworkAccess;
                    if (current == NetworkAccess.Internet)
                    {
                        HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.GetUserPerformanceUrl, SelectedUsers.Id, SelectedCampaign.Id, 0), string.Empty);
                        var response = await apicall.Get<PerformanceModel>();
                        if (response != null && response.Result && response.Object != null && response.Message.Equals("Success"))
                        {
                            PerformanceData = response.Object;
                            IsViewPerformaceVisible = true;
                            MessagingCenter.Send<PerformanceViewModel>(this, "ModelFilled");
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
        }
        private async Task<bool> Validation()
        {
            if (SelectedCampaign == null)
            {
                await ShowAlert("Please select campaign.");
                return false;
            }
            else if (SelectedUsers == null)
            {
                await ShowAlert("Please select user.");
                return false;
            }

            return true;
        }
        public void ExecuteOnDay1(object obj)
        {
            Day1Color = Color.FromHex("#2baae1");
            Day7Color = Day30Color = Color.FromHex("#4C4C4C");
        }
        public void ExecuteOnDay7(object obj)
        {
            Day7Color = Color.FromHex("#2baae1");
            Day1Color = Day30Color = Color.FromHex("#4C4C4C");
        }
        public void ExecuteOnDay30(object obj)
        {
            Day30Color = Color.FromHex("#2baae1");
            Day7Color = Day1Color = Color.FromHex("#4C4C4C");
        }
        public async void ExecuteOnViewUserPerformance(object obj)
        {
            int count = 0;
            if (Day1Color == Color.FromHex("#2baae1"))
                count = 1;
            else if (Day7Color == Color.FromHex("#2baae1"))
                count = 7;
            else if (Day30Color == Color.FromHex("#2baae1"))
                count = 30;
            await _navigation.PushAsync(new LeadsCalled(count));
        }
        #endregion
    }
}
