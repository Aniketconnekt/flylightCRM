using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model;
using CRM.Model.AdminModel;
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
    public class TransferLeadViewModel : BaseViewModel
    {
        #region CTOR
        public TransferLeadViewModel(INavigation navigation) : base(navigation)
        {
        }
        #endregion

        #region Commands
        public DelegateCommand TransferCommand => new DelegateCommand(ExecuteOnTransfer);
        #endregion

        #region Properties
        private List<NewLeadsData> _newLeadsData;
        public List<NewLeadsData> NewLeadsData
        {
            get => _newLeadsData;
            set { _newLeadsData = value; OnPropertyChanged(); }
        }

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
            set
            {
                _selectedCampaign = value; OnPropertyChanged();
                CampaignName = _selectedCampaign.Name;
                BindUsersByCampId();
            }
        }

        private List<CampUsersData> _usersList;
        public List<CampUsersData> UsersList
        {
            get => _usersList;
            set { _usersList = value; OnPropertyChanged(); }
        }

        private CampUsersData _selectedUser;
        public CampUsersData SelectedUser
        {
            get => _selectedUser;
            set { _selectedUser = value; OnPropertyChanged(); }
        }

        private string _campaignName;
        public string CampaignName
        {
            get => _campaignName;
            set { _campaignName = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async void Initialize(List<NewLeadsData> newLeadsDataList)
        {
            ShowLoading();
            NewLeadsData = newLeadsDataList;

            var data = await Task.Run(async () => await GetCampaignList());
            CampaignList = data.ToList();

            SelectedCampaign = CampaignList.Find(c => c.Id == newLeadsDataList.FirstOrDefault().CampaignId);
            CampaignName = SelectedCampaign.Name;
        }
        public async void BindUsersByCampId()
        {
            var data = await Task.Run(async () => await GetUsersByCampId());
            UsersList = data.ToList();
        }
        public async Task<ObservableCollection<CampUsersData>> GetUsersByCampId()
        {
            var list = new ObservableCollection<CampUsersData>();
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.GetUserByCampIdUrl, SelectedCampaign.Id), string.Empty);
                    var response = await apicall.Get<CampUsers>();
                    if (response != null && response.Result && response.List != null && response.Message.Equals("Success"))
                    {
                        foreach (var user in response.List)
                            list.Add(user);
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
        public async void ExecuteOnTransfer(object obj)
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {

                    string transferLeads = String.Empty;
                    foreach (var del in NewLeadsData)
                        transferLeads += del.LeadId.ToString() + ",";

                    transferLeads = transferLeads.Remove(transferLeads.Length - 1, 1);

                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.TransferLeadUrl, transferLeads, SelectedUser.Id), string.Empty);
                    var response = await apicall.Get<bool>();
                    if (response)
                    {
                        HideLoading();
                        await _navigation.PushAsync(new View.Successfull.SuccessfullPage());
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
            catch (Exception ex)
            {
                HideLoading();
                await ShowAlert(ex.Message);
            }
        }
        #endregion
    }
}
