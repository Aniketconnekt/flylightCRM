using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model;
using CRM.Model.AdminModel;
using CRM.SuperAdmin.Model;
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
    public class LeadAllottmentViewModel : BaseViewModel
    {
        #region Data Members
        public List<LeadAllottmentData> LeadAllottmentDataList = new List<LeadAllottmentData>();
        #endregion

        #region CTOR
        public LeadAllottmentViewModel(INavigation navigation) : base(navigation)
        {
        }
        #endregion

        #region Commands
        public DelegateCommand SearchCommand => new DelegateCommand(ExecuteOnSearch);
        public DelegateCommand AllotCommand => new DelegateCommand(ExecuteOnAllot);
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

        private bool _isDetailVisible;
        public bool IsDetailVisible
        {
            get => _isDetailVisible;
            set { _isDetailVisible = value; OnPropertyChanged(); }
        }

        private bool _isSearchVisible;
        public bool IsSearchVisible
        {
            get => _isSearchVisible;
            set { _isSearchVisible = value; OnPropertyChanged(); }
        }

        private ObservableCollection<LeadAllottmentData> _leadAllottmentData;
        public ObservableCollection<LeadAllottmentData> LeadAllottmentData
        {
            get => _leadAllottmentData;
            set { _leadAllottmentData = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async Task Initialize()
        {
            ShowLoading();
            var data = await Task.Run(async () => await GetCampaignData());
            CampaignList = data.ToList();
            var userData = await Task.Run(async () => await GetUsersData());
            UsersList = userData.ToList();

            IsDetailVisible = false;
            IsSearchVisible = true;
            LeadAllottmentData = new ObservableCollection<LeadAllottmentData>();
        }
        public async void ExecuteOnSearch(object obj)
        {
            if (SelectedCampaign != null)
            {
                ShowLoading();
                LeadAllottmentData = await GetLeadAllottmentData();
                if (LeadAllottmentData.Any())
                {
                    IsSearchVisible = false;
                    IsDetailVisible = true;
                }
            }
            else
                await ShowAlert("Please select campaign.");
        }
        public async Task<ObservableCollection<LeadAllottmentData>> GetLeadAllottmentData()
        {
            var list = new ObservableCollection<LeadAllottmentData>();
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    var userId = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.SearchLeadsbycampidUrl, userId, SelectedCampaign.Id), string.Empty);
                    var response = await apicall.Get<LeadAllottmentModel>();
                    if (response != null && response.Result && response.List != null && response.Message.Equals("Success"))
                    {
                        foreach (var allot in response.List)
                            list.Add(allot);
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
        public async void ExecuteOnAllot(object obj)
        {
            try
            {
                if (LeadAllottmentDataList.Count() == 0)
                    await ShowAlert("Please select atleast single lead.");
                else if(SelectedUsers == null)
                    await ShowAlert("Please select user.");
                else
                {
                    ShowLoading();
                    var current = Connectivity.NetworkAccess;
                    if (current == NetworkAccess.Internet)
                    {
                        string allotLeads = String.Empty;
                        foreach (var all in LeadAllottmentDataList)
                            allotLeads += all.Id.ToString() + ",";

                        allotLeads = allotLeads.Remove(allotLeads.Length - 1, 1);
                        HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.AllotLeadsToUserUrl, allotLeads, SelectedUsers.Id), string.Empty);
                        var response = await apicall.Get<string>();
                        if (response.Equals("Success"))
                        {
                            HideLoading();
                            await ShowAlert("Lead allotted successfully.");
                            await _navigation.PushAsync(new View.Admin.Lead.Leads());
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
            catch (Exception ex)
            {
                HideLoading();
                await ShowAlert(ex.Message);
            }
        }
        #endregion
    }
}
