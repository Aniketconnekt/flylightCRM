using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model.AdminModel;
using CRM.SuperAdmin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.ViewModel.AdminViewModel
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
        private List<UsersData> _usersList;
        public List<UsersData> UsersList
        {
            get => _usersList;
            set { _usersList = value; OnPropertyChanged(); }
        }

        private UsersData _selectedFromUser;
        public UsersData SelectedFromUser
        {
            get => _selectedFromUser;
            set { _selectedFromUser = value; OnPropertyChanged(); }
        }

        private UsersData _selectedToUser;
        public UsersData SelectedToUser
        {
            get => _selectedToUser;
            set { _selectedToUser = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async void Initialize()
        {
            ShowLoading();
            var userData = await Task.Run(async () => await GetUsersData());
            UsersList = userData.ToList();
        }
        public async void ExecuteOnTransfer(object obj)
        {
            if(await Validation())
            {
                try
                {
                    ShowLoading();
                    var current = Connectivity.NetworkAccess;
                    if (current == NetworkAccess.Internet)
                    {
                        HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.TransferAllLeadToAnotherUserUrl, Settings.CRM_UserId, SelectedFromUser.Id, SelectedToUser.Id), string.Empty);
                        var response = await apicall.Get<TransferLeadModel>();
                        if (response != null && response.Result && response.Object != null && response.Message.Equals("Success"))
                        {
                            HideLoading();
                            await _navigation.PushAsync(new View.Admin.Successful.Successful());
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
                    HideLoading();
                    await ShowAlert(ex.Message);
                }
            }
        }
        private async Task<bool> Validation()
        {
            if(SelectedFromUser == null)
            {
                await ShowAlert("Please select transfer from user.");
                return false;
            }
            else if (SelectedToUser == null)
            {
                await ShowAlert("Please select transfer to user.");
                return false;
            }
            else if (SelectedFromUser.Equals(SelectedToUser))
            {
                await ShowAlert("Please select different users.");
                return false;
            }

            return true;
        }
        #endregion
    }
}
