using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model;
using CRM.Model.AdminModel;
using CRM.View.Admin.Compaign;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.ViewModel.AdminViewModel
{
    public class CampaignViewModel : BaseViewModel
    {
        #region Data Members
        public List<CampaignData> CampaignDataList = new List<CampaignData>();
        #endregion

        #region CTOR
        public CampaignViewModel(INavigation navigation) : base(navigation)
        {

        }
        #endregion

        #region Commands
        public DelegateCommand AddCampaignCommand => new DelegateCommand(ExecuteOnAddCampaign);
        public DelegateCommand UpdateCampaignCommand => new DelegateCommand(ExecuteOnUpdateCampaign);
        public DelegateCommand DeleteCampaignCommand => new DelegateCommand(ExecuteOnDeleteCampaign);
        public DelegateCommand CampaignSearch => new DelegateCommand(ExecuteOnCampaignSearch);
        #endregion

        #region Properties
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
            ShowLoading();
            CampaignData = await GetCampaignData();
            CampaignDataList = new List<CampaignData>();
        }
        public async void ExecuteOnAddCampaign(object obj)
        {
            await _navigation.PushAsync(new AddCampaign(null));
        }
        public async void ExecuteOnUpdateCampaign(object obj)
        {
            if (CampaignDataList.Count() == 0)
                await ShowAlert("Please select a campaign for update.");
            else if (CampaignDataList.Count() > 1)
                await ShowAlert("Please select single campaign for update.");
            else
                App.MasterDetailPage.Detail = new NavigationPage(new AddCampaign(CampaignDataList.FirstOrDefault()))
                {
                    BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                    BarTextColor = Color.FromHex(App.nav_bar_text_color)
                };
        }
        public async void ExecuteOnDeleteCampaign(object obj)
        {
            try
            {
                if (CampaignDataList.Count() == 0)
                    await ShowAlert("Please select a campaign for delete.");
                else
                {
                    var result = await UserDialogs.Instance.ConfirmAsync("Do you want to delete?", "", "Yes", "No");
                    if (result)
                    {
                        ShowLoading();
                        var current = Connectivity.NetworkAccess;
                        if (current == NetworkAccess.Internet)
                        {
                            string delCampaign = String.Empty;
                            foreach (var del in CampaignDataList)
                                delCampaign += del.Id.ToString() + ",";

                            delCampaign = delCampaign.Remove(delCampaign.Length - 1, 1);

                            HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.DeleteCampaignUrl, delCampaign), string.Empty);
                            var response = await apicall.Get<bool>();
                            if (response)
                            {
                                HideLoading();
                                await ShowAlert("Campaign(s) deleted successfully.");
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
        public async void ExecuteOnCampaignSearch(object obj)
        {
            CampaignData = await GetSearchCampaignData(obj as string);
        }
        private async Task<ObservableCollection<CampaignData>> GetSearchCampaignData(string searchValue)
        {
            var list = new ObservableCollection<CampaignData>();
            try
            {
                ShowLoading();
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    var userId = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.SearchCampaignListUrl, userId, searchValue), string.Empty);
                    var response = await apicall.Get<CampaignModel>();
                    if (response != null && response.Result && response.List != null)
                    {
                        foreach (var campaigns in response.List)
                            list.Add(campaigns);
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
                await ShowAlert(ex.Message);
                HideLoading();
            }
            HideLoading();
            return list;
        }
        #endregion
    }
}
