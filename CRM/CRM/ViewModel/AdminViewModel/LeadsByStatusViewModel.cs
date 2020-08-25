using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
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

namespace CRM.ViewModel.AdminViewModel
{
    public class LeadsByStatusViewModel : BaseViewModel
    {
        #region CTOR
        public LeadsByStatusViewModel(INavigation navigation) : base(navigation)
        {
        }
        #endregion

        #region Commands
        public DelegateCommand SearchCommand => new DelegateCommand(ExecuteOnSearch);
        public DelegateCommand LoadMoreItemsCommand => new DelegateCommand(ExecuteOnLoadMoreItems);
        #endregion

        #region Properties
        private List<StatusData> _statusList;
        public List<StatusData> StatusList
        {
            get => _statusList;
            set { _statusList = value; OnPropertyChanged(); }
        }

        private StatusData _selectedStatus;
        public StatusData SelectedStatus
        {
            get => _selectedStatus;
            set { _selectedStatus = value; OnPropertyChanged(); }
        }

        private List<UsersData> _usersList;
        public List<UsersData> UsersList
        {
            get => _usersList;
            set { _usersList = value; OnPropertyChanged(); }
        }

        private UsersData _selectedUser;
        public UsersData SelectedUser
        {
            get => _selectedUser;
            set { _selectedUser = value; OnPropertyChanged(); }
        }

        private ObservableCollection<LeadsData> _leadsByStatusList;
        public ObservableCollection<LeadsData> LeadsByStatusList
        {
            get => _leadsByStatusList;
            set { _leadsByStatusList = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async Task Initialize()
        {
            ShowLoading();
            var statusList = await GetStatusData();
            StatusList = statusList.ToList();

            var userData = await Task.Run(async () => await GetUsersData());
            UsersList = userData.ToList();
        }
        private async Task<ObservableCollection<StatusData>> GetStatusData()
        {
            var list = new ObservableCollection<StatusData>();
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    var userId = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.GetstatusListUrl, userId), string.Empty);
                    var response = await apicall.Get<GetStatus>();
                    if (response != null && response.Result && response.List != null)
                    {
                        foreach (var status in response.List)
                            list.Add(status);

                        var data = new ObservableCollection<StatusData>(list.OrderByDescending(o => o.CreationDate));
                        list = data;
                    }
                    else
                    {
                        HideLoading();
                        await ShowAlert("Record Not Found.");
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
            return list;
        }
        public async void ExecuteOnSearch(object obj)
        {
            LeadsByStatusList = new ObservableCollection<LeadsData>();
            await GetLeadsData(0);
            if (!LeadsByStatusList.Any())
                await ShowAlert("Record Not Found");
        }
        private async void ExecuteOnLoadMoreItems(object obj)
        {
            if (LeadsByStatusList.Any())
                await GetLeadsData(LeadsByStatusList.LastOrDefault().Id);
        }
        public async Task GetLeadsData(int lastrecordsid)
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    ShowLoading();
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.GetLeadsListForAdminbyStatusIdUrl, SelectedUser.Id, SelectedStatus.Id, lastrecordsid), string.Empty);
                    var response = await apicall.Get<LeadsModel>();
                    if (response != null && response.Result && response.List != null && response.Message.Equals("Success"))
                    {
                        foreach (var lead in response.List)
                            LeadsByStatusList.Add(lead);

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
        #endregion
    }
}
