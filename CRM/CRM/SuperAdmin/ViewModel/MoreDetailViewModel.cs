using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.SuperAdmin.Model;
using CRM.SuperAdmin.View.Performance;
using CRM.ViewModel;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.SuperAdmin.ViewModel
{
    public class MoreDetailViewModel : BaseViewModel
    {
        AdminCustomerData _adminCustomerData = new AdminCustomerData();

        #region CTOR
        public MoreDetailViewModel(INavigation navigation) : base(navigation)
        {
        }
        #endregion

        #region Commands
        public DelegateCommand BackCommand => new DelegateCommand(ExecuteOnBack);
        public DelegateCommand CampaignsCommand => new DelegateCommand(ExecuteOnCampaigns);
        public DelegateCommand TotalUsersCommand => new DelegateCommand(ExecuteOnTotalUsers);
        #endregion

        #region Properties
        private MoreDetailData _moreDetailData = new MoreDetailData();
        public MoreDetailData MoreDetailData
        {
            get => _moreDetailData;
            set { _moreDetailData = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async Task Initialize(AdminCustomerData adminCustomerData)
        {
            try
            {
                _adminCustomerData = adminCustomerData;
                ShowLoading();
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.GetAdminMoredetailUrl,adminCustomerData.Id), string.Empty);
                    var response = await apicall.Get<MoreDetailModel>();
                    if (response != null && response.Result && response.Object != null)
                    {
                        MoreDetailData = response.Object;
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
                await ShowAlert(ex.Message);
                HideLoading();
            }
        }
        public async void ExecuteOnBack(object obj)
        {
            await _navigation.PopAsync();
        }
        public async void ExecuteOnCampaigns(object obj)
        {
            await _navigation.PushAsync(new CampaignsPage(_adminCustomerData));
        }
        public async void ExecuteOnTotalUsers(object obj)
        {
            await _navigation.PushAsync(new UsersPage(_adminCustomerData));
        }
        #endregion
    }
}
