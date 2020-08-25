using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model;
using CRM.View.Successfull;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.ViewModel
{
    public class CreditFormViewModel : BaseViewModel
    {
        #region CTOR
        public CreditFormViewModel(INavigation navigation) : base(navigation)
        {
        }
        #endregion

        #region Commands
        public DelegateCommand SubmitCommand => new DelegateCommand(ExecuteOnSubmit);
        #endregion

        #region Properties
        private NewLeadsData _newLeadsData;
        public NewLeadsData NewLeadsData
        {
            get => _newLeadsData;
            set { _newLeadsData = value; OnPropertyChanged(); }
        }

        private CreditFormModel _creditFormModel = new CreditFormModel();
        public CreditFormModel CreditFormModel
        {
            get => _creditFormModel;
            set { _creditFormModel = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public void Initialize(NewLeadsData newLeadsData)
        {
            NewLeadsData = newLeadsData;
        }
        public async void ExecuteOnSubmit(object obj)
        {
            if(await Validation())
            {
                try
                {
                    var current = Connectivity.NetworkAccess;
                    if (current == NetworkAccess.Internet)
                    {
                        ShowLoading();
                        HttpClientHelper apicall = new HttpClientHelper(ApiUrls.SaveLeadCreditsUrl, string.Empty);
                        CreditFormModel.CreatedById = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                        CreditFormModel.LeadId = NewLeadsData.LeadId;
                        var json = JsonConvert.SerializeObject(CreditFormModel);
                        var response = await apicall.Post<bool>(json);
                        if (response)
                        {
                            HideLoading();
                            await _navigation.PushAsync(new SuccessfullPage());
                            CreditFormModel = new CreditFormModel();
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
        }
        private async Task<bool> Validation()
        {
            if (string.IsNullOrWhiteSpace(CreditFormModel.CreditAmount))
            {
                await ShowAlert("Please enter credit amount.");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(CreditFormModel.Desciption))
            {
                await ShowAlert("Please enter desciption.");
                return false;
            }

            return true;
        }
        #endregion
    }
}
