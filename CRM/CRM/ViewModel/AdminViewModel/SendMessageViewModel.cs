using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model.AdminModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.ViewModel.AdminViewModel
{
    public class SendMessageViewModel : BaseViewModel
    {
        #region CTOR
        public SendMessageViewModel(INavigation navigation) : base(navigation)
        {
        }
        #endregion

        #region Commands
        public DelegateCommand SendCommand => new DelegateCommand(ExecuteOnSend);
        #endregion

        #region Properties
        //private List<CampaignData> _campaignList;
        //public List<CampaignData> CampaignList
        //{
        //    get => _campaignList;
        //    set { _campaignList = value; OnPropertyChanged(); }
        //}

        //private CampaignData _selectedCampaign;
        //public CampaignData SelectedCampaign
        //{
        //    get => _selectedCampaign;
        //    set { _selectedCampaign = value; OnPropertyChanged(); }
        //}

        private string _message;
        public string Message
        {
            get => _message;
            set { _message = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        //public async void Initialize()
        //{
        //    ShowLoading();
        //    var data = await GetCampaignData();
        //    CampaignList = data.ToList();
        //}
        //public async void ExecuteOnSend(object obj)
        //{
        //    try
        //    {
        //        if (SelectedCampaign == null)
        //        {
        //            await ShowAlert("Please select campaign.");
        //            return;
        //        }
        //        else if (string.IsNullOrWhiteSpace(Message))
        //        {
        //            await ShowAlert("Please enter message.");
        //            return;
        //        }

        //        ShowLoading();
        //        var current = Connectivity.NetworkAccess;
        //        if (current == NetworkAccess.Internet)
        //        {
        //            HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.SendMessageToLeadsUrl, Message, SelectedCampaign.Id), string.Empty);
        //            var response = await apicall.Get<bool>();
        //            if (response)
        //            {
        //                HideLoading();
        //                await _navigation.PushAsync(new View.Admin.Successful.Successful());
        //            }
        //            else
        //            {
        //                HideLoading();
        //                await ShowAlert(AppConstant.SOMETHING_WRONG);
        //            }
        //        }
        //        else
        //        {
        //            HideLoading();
        //            await UserDialogs.Instance.AlertAsync(AppConstant.NETWORK_FAILURE, "", "Ok");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await ShowAlert(ex.Message);
        //        HideLoading();
        //    }
        //}

        public async void ExecuteOnSend(object obj)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Title = "Share FlylightCRM",
                Text = Message
            });
        }
        #endregion
    }
}
