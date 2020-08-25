using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model.AdminModel;
using CRM.View.Admin.Lead;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.ViewModel.AdminViewModel
{
    public class LeadsViewModel : BaseViewModel
    {
        #region Data Members
        public List<LeadsData> LeadsDataList = new List<LeadsData>();
        #endregion

        #region CTOR
        public LeadsViewModel(INavigation navigation) : base(navigation)
        {
        }
        #endregion

        #region Commands
        public DelegateCommand AddNewLeadsCommand => new DelegateCommand(ExecuteOnAddNewLeads);
        public DelegateCommand UpdateLeadCommand => new DelegateCommand(ExecuteOnUpdateLead);
        public DelegateCommand DeleteLeadCommand => new DelegateCommand(ExecuteOnDeleteLead);
        public DelegateCommand LeadsSearch => new DelegateCommand(ExecuteOnLeadsSearch);
        public DelegateCommand LoadMoreItemsCommand => new DelegateCommand(ExecuteOnLoadMoreItems);
        #endregion

        #region Properties
        private ObservableCollection<LeadsData> _leadsData;
        public ObservableCollection<LeadsData> LeadsData
        {
            get => _leadsData;
            set { _leadsData = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async Task Initialize()
        {
            LeadsData = new ObservableCollection<LeadsData>();
            await GetLeadsData(0);
            if(!LeadsData.Any())
                await ShowAlert("Record Not Found");
            LeadsDataList = new List<LeadsData>();
        }
        private async void ExecuteOnLoadMoreItems(object obj)
        {
            if(LeadsData.Any())
                await GetLeadsData(LeadsData.LastOrDefault().Id);
        }
        public async Task GetLeadsData(int lastrecordsid)
        {
            try
            {
                ShowLoading();
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    var userId = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.GetLeadsListForAdminUrl, userId, lastrecordsid), string.Empty);
                    var response = await apicall.Get<LeadsModel>();
                    if (response != null && response.Result && response.List != null && response.Message.Equals("Success"))
                    {
                        foreach (var lead in response.List)
                            LeadsData.Add(lead);

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
        public void ExecuteOnAddNewLeads(object obj)
        {
            App.MasterDetailPage.Detail = new NavigationPage(new AddLead(null))
            {
                BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                BarTextColor = Color.FromHex(App.nav_bar_text_color),
            };
        }
        public async void ExecuteOnUpdateLead(object obj)
        {
            if (LeadsDataList.Count() == 0)
                await ShowAlert("Please select a lead for update.");
            else if (LeadsDataList.Count() > 1)
                await ShowAlert("Please select single lead for update.");
            else
                App.MasterDetailPage.Detail = new NavigationPage(new AddLead(LeadsDataList.FirstOrDefault()))
                {
                    BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                    BarTextColor = Color.FromHex(App.nav_bar_text_color)
                };
        }
        public async void ExecuteOnDeleteLead(object obj)
        {
            try
            {
                if (LeadsDataList.Count() == 0)
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
                            foreach (var del in LeadsDataList)
                                delLeads += del.Id.ToString() + ",";

                            delLeads = delLeads.Remove(delLeads.Length - 1, 1);

                            HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.DeleteLeadsUrl, delLeads), string.Empty);
                            var response = await apicall.Get<bool>();
                            if (response)
                            {
                                HideLoading();
                                await ShowAlert("Leads(s) deleted successfully.");
                                await Initialize();
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
        public async void ExecuteOnLeadsSearch(object obj)
        {
            LeadsData = new ObservableCollection<LeadsData>();
            LeadsData = await GetSearchLeadsData(obj as string);
            LeadsDataList = new List<LeadsData>();
        }
        public async Task<ObservableCollection<LeadsData>> GetSearchLeadsData(string searchValue)
        {
            var list = new ObservableCollection<LeadsData>();
            try
            {
                ShowLoading();
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    var userId = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.SearchLeadsForAdminUrl, userId, searchValue), string.Empty);
                    var response = await apicall.Get<LeadsModel>();
                    if (response != null && response.Result && response.List != null && response.Message.Equals("Success"))
                    {
                        foreach (var lead in response.List)
                            list.Add(lead);

                        var data = new ObservableCollection<LeadsData>(list.OrderByDescending(o => o.CreationDate));
                        list = data;
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
        #endregion
    }
}
