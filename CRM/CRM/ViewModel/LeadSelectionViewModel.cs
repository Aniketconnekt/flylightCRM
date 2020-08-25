using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model;
using CRM.View.AddLead;
using CRM.View.TransferLead;
using CRM.View.UpdateInformation;
using Syncfusion.ListView.XForms;
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
    public class LeadSelectionViewModel : BaseViewModel
    {
        #region Data Members
        public List<NewLeadsData> UsersDatasList = new List<NewLeadsData>();
        Tuple<int, int, string, string> _tuple;
        #endregion

        #region CTOR
        public LeadSelectionViewModel(INavigation navigation) : base(navigation)
        {
        }
        #endregion

        #region Commands
        public DelegateCommand TransferLeadCommand => new DelegateCommand(ExecuteOnTransferLead);
        public DelegateCommand DeleteLeadCommand => new DelegateCommand(ExecuteOnDeleteLead);
        public DelegateCommand EditLeadCommand => new DelegateCommand(ExecuteOnEditLead);
        public DelegateCommand AddLeadCommand => new DelegateCommand(ExecuteOnAddLead);
        public DelegateCommand LoadMoreItemsCommand => new DelegateCommand(ExecuteOnLoadMoreItems);
        #endregion

        #region Properties
        private ObservableCollection<NewLeadsData> _leadsData;
        public ObservableCollection<NewLeadsData> LeadsData
        {
            get => _leadsData;
            set { _leadsData = value; OnPropertyChanged(); }
        }

        private bool _isDeleteEditVisible = false;
        public bool IsDeleteEditVisible
        {
            get => _isDeleteEditVisible;
            set { _isDeleteEditVisible = value; OnPropertyChanged(); }
        }

        #endregion

        #region Methods
        public async Task Initialize(Tuple<int, int, string, string> tuple)
        {
            LeadsData = new ObservableCollection<NewLeadsData>();
            if (tuple != null)
            {
                _tuple = tuple;
                await GetLeadsData(_tuple, 0);
                if (!LeadsData.Any())
                    await ShowAlert("Record Not Found");
            }
            else
            {
                IsDeleteEditVisible = true;
                await GetLeadsByUserData(0);
                if (!LeadsData.Any())
                    await ShowAlert("Record Not Found");
            }
            UsersDatasList = new List<NewLeadsData>();
        }
        private async void ExecuteOnLoadMoreItems(object obj)
        {
            if (_tuple != null && LeadsData.Any())
                await GetLeadsData(_tuple, LeadsData.LastOrDefault().Id);
            else if (LeadsData.Any())
                await GetLeadsByUserData(LeadsData.LastOrDefault().Id);
        }
        public async Task GetLeadsData(Tuple<int, int, string, string> tuple, int lastrecordsid)
        {
            try
            {
                ShowLoading();
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    var userId = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.SearchleadslistUrl, tuple.Item1, tuple.Item4, tuple.Item3, tuple.Item2, userId, lastrecordsid), string.Empty);
                    var response = await apicall.Get<Leads>();
                    if (response != null && response.Result && response.List != null && response.Message.Equals("Success"))
                    {
                        foreach (var lead in response.List)
                            LeadsData.Add(lead);

                        if (response.List.Count() < 20 || response.List.Count() == response.TotalCount)
                            LoadMoreOption = LoadMoreOption.None;
                        else
                            LoadMoreOption = LoadMoreOption.Manual;
                    }
                    else
                        LoadMoreOption = LoadMoreOption.None;
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
        public async Task GetLeadsByUserData(int lastrecordsid)
        {
            try
            {
                ShowLoading();
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    var userId = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.GetLeadsForUsersUrl, userId, lastrecordsid), string.Empty);
                    var response = await apicall.Get<Leads>();
                    if (response != null && response.Result && response.List != null && response.Message.Equals("Success"))
                    {
                        foreach (var lead in response.List)
                            LeadsData.Add(lead);

                        if (response.List.Count() < 20 || response.List.Count() == response.TotalCount)
                            LoadMoreOption = LoadMoreOption.None;
                        else
                            LoadMoreOption = LoadMoreOption.Manual;
                    }
                    else
                        LoadMoreOption = LoadMoreOption.None;
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
        public async void ExecuteOnDeleteLead(object obj)
        {
            try
            {
                if (UsersDatasList.Count() == 0)
                    await ShowAlert("Please select a lead for delete.");
                else
                {
                    var result = await UserDialogs.Instance.ConfirmAsync("Do you want to delete?", "", "Yes", "No");
                    if (result)
                    {
                        ShowLoading();
                        var current = Connectivity.NetworkAccess;
                        if (current == NetworkAccess.Internet)
                        {
                            string delLeads = String.Empty;
                            foreach (var del in UsersDatasList)
                                delLeads += del.Id.ToString() + ",";

                            delLeads = delLeads.Remove(delLeads.Length - 1, 1);

                            HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.DeleteLeadsUrl, delLeads), string.Empty);
                            var response = await apicall.Get<bool>();
                            if (response)
                            {
                                HideLoading();
                                await ShowAlert("Leads(s) deleted successfully.");
                                await Initialize(null);
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
            }
            catch (Exception ex)
            {
                HideLoading();
                await ShowAlert(ex.Message);
            }
        }
        public async void ExecuteOnTransferLead(object obj)
        {
            if (UsersDatasList.Any())
                App.MasterDetailPage.Detail = new NavigationPage(new TransferLeadPage(UsersDatasList))
                {
                    BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                    BarTextColor = Color.FromHex(App.nav_bar_text_color),
                };
            else
                await ShowAlert("Please select atleast one lead.");
        }
        public async void ExecuteOnEditLead(object obj)
        {
            if (UsersDatasList.Count() == 0)
                await ShowAlert("Please select a lead for update.");
            else if (UsersDatasList.Count() > 1)
                await ShowAlert("Please select single lead for update.");
            else
                App.MasterDetailPage.Detail = new NavigationPage(new UpdateInformationPage(UsersDatasList, "UpdateUserLead"))
                {
                    BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                    BarTextColor = Color.FromHex(App.nav_bar_text_color),
                };
        }
        public void ExecuteOnAddLead(object obj)
        {
            App.MasterDetailPage.Detail = new NavigationPage(new AddLeadPage())
            {
                BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                BarTextColor = Color.FromHex(App.nav_bar_text_color),
            };
        }
        #endregion
    }
}
