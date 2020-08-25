using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.SuperAdmin.Model;
using CRM.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.SuperAdmin.ViewModel
{
    public class CampaignsViewModel : BaseViewModel
    {
        AdminCustomerData _adminCustomerData;

        #region CTOR
        public CampaignsViewModel(INavigation navigation) : base(navigation)
        {
        }
        #endregion

        #region Commands
        public DelegateCommand CampaignSearch => new DelegateCommand(ExecuteOnCampaignSearch);
        #endregion

        #region Properties
        private string _userName;
        public string UserName
        {
            get => _userName;
            set { _userName = value; OnPropertyChanged(); }
        }

        private ObservableCollection<CampaignsData> _campaignsData;
        public ObservableCollection<CampaignsData> CampaignsData
        {
            get => _campaignsData;
            set { _campaignsData = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async void Initialize(AdminCustomerData adminCustomerData)
        {
            UserName = adminCustomerData.UserName;
            _adminCustomerData = adminCustomerData;
            CampaignsData = await GetCampaignsData(adminCustomerData);
        }
        private async Task<ObservableCollection<CampaignsData>> GetCampaignsData(AdminCustomerData adminCustomerData)
        {
            var list = new ObservableCollection<CampaignsData>();
            try
            {
                ShowLoading();
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.GetcampdetailbyuseridUrl, adminCustomerData.Id), string.Empty);
                    var response = await apicall.Get<CampaignsModel>();
                    if (response != null && response.Result && response.List != null && response.Message.Equals("Success"))
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
        public async void ExecuteOnCampaignSearch(object obj)
        {
            CampaignsData = await GetSearchCampaignData(obj as string);
        }
        private async Task<ObservableCollection<CampaignsData>> GetSearchCampaignData(string searchValue)
        {
            var list = new ObservableCollection<CampaignsData>();
            try
            {
                ShowLoading();
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.SearchCampaignListUrl, _adminCustomerData.Id, searchValue), string.Empty);
                    var response = await apicall.Get<CampaignsModel>();
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
