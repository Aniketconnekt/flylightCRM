using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model;
using CRM.View.Successfull;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.ViewModel
{
    public class SendReminderViewModel : BaseViewModel
    {
        #region CTOR
        public SendReminderViewModel(INavigation navigation) : base(navigation)
        {
        }
        #endregion

        #region Commands
        public DelegateCommand SendCommand => new DelegateCommand(ExecuteOnSend);
        #endregion

        #region Properties
        private NewLeadsData _newLeadsData;
        public NewLeadsData NewLeadsData
        {
            get => _newLeadsData;
            set { _newLeadsData = value; OnPropertyChanged(); }
        }

        private string _meaasge;
        public string Meaasge
        {
            get => _meaasge;
            set { _meaasge = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public void Initialize(NewLeadsData newLeadsData)
        {
            NewLeadsData = newLeadsData;
        }
        public async void ExecuteOnSend(object obj)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(Meaasge))
                {
                    await ShowAlert("Please enter message.");
                    return;
                }

                ShowLoading();
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.SendReminderUrl, NewLeadsData.LeadId, Meaasge), string.Empty);
                    var response = await apicall.Get<bool>();
                    if (response)
                    {
                        HideLoading();
                        await _navigation.PushAsync(new SuccessfullPage());
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
                await ShowAlert(ex.Message);
                HideLoading();
            }
        }
        #endregion
    }
}
