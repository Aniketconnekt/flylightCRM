using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.SuperAdmin.Model;
using CRM.View.Admin.Users;
using CRM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.ViewModel.AdminViewModel
{
    public class UserViewModel : BaseViewModel
    {
        #region Data Members
        public List<UsersData> UsersDatasList = new List<UsersData>();
        #endregion

        #region CTOR
        public UserViewModel(INavigation navigation) : base(navigation)
        {
        }
        #endregion

        #region Commands
        public DelegateCommand AddNewUserCommand => new DelegateCommand(ExecuteOnAddNewUser);
        public DelegateCommand UpdateUserCommand => new DelegateCommand(ExecuteOnUpdateUser);
        public DelegateCommand DeleteUserCommand => new DelegateCommand(ExecuteOnDeleteUser);
        public DelegateCommand ViewLeadsCommand => new DelegateCommand(ExecuteOnViewLeads);
        public DelegateCommand UsersSearch => new DelegateCommand(ExecuteOnUsersSearch);
        #endregion

        #region Properties
        private ObservableCollection<UsersData> _usersData;
        public ObservableCollection<UsersData> UsersData
        {
            get => _usersData;
            set { _usersData = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async Task Initialize()
        {
            ShowLoading();
            UsersData = await GetUsersData();
            UsersDatasList = new List<UsersData>();
        }
        public void ExecuteOnAddNewUser(object obj)
        {
            App.MasterDetailPage.Detail = new NavigationPage(new AddUser(null))
            {
                BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                BarTextColor = Color.FromHex(App.nav_bar_text_color)
            };
        }
        public async void ExecuteOnUpdateUser(object obj)
        {
            if (UsersDatasList.Count() == 0)
                await ShowAlert("Please select a user for update.");
            else if (UsersDatasList.Count() > 1)
                await ShowAlert("Please select single user for update.");
            else
                App.MasterDetailPage.Detail = new NavigationPage(new AddUser(UsersDatasList.FirstOrDefault()))
                {
                    BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                    BarTextColor = Color.FromHex(App.nav_bar_text_color)
                };
        }
        public async void ExecuteOnDeleteUser(object obj)
        {
            try
            {
                if (UsersDatasList.Count() == 0)
                    await ShowAlert("Please select a user for delete.");
                else
                {
                    var result = await UserDialogs.Instance.ConfirmAsync("Do you want to delete?", "", "Yes", "No");
                    if (result)
                    {
                        ShowLoading();
                        var current = Connectivity.NetworkAccess;
                        if (current == NetworkAccess.Internet)
                        {
                            string delUsers = String.Empty;
                            foreach (var del in UsersDatasList)
                                delUsers += del.Id.ToString() + ",";

                            delUsers = delUsers.Remove(delUsers.Length - 1, 1);

                            HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.DeleteUserUrl, delUsers), string.Empty);
                            var response = await apicall.Get<bool>();
                            if (response)
                            {
                                HideLoading();
                                await ShowAlert("User(s) deleted successfully.");
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
        public async void ExecuteOnViewLeads(object obj)
        {
            var data = obj as UsersData;
            await _navigation.PushAsync(new View.Admin.Lead.LeadsCalledAddLead(data.Id));
        }
        public async void ExecuteOnUsersSearch(object obj)
        {
            UsersData = await GetSearchUsersData(obj as string);
        }
        public async Task<ObservableCollection<UsersData>> GetSearchUsersData(string searchValue)
        {
            var list = new ObservableCollection<UsersData>();
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    var userId = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.SearchUsersUrl, userId, searchValue), string.Empty);
                    var response = await apicall.Get<UsersModel>();
                    if (response != null && response.Result && response.List != null && response.Message.Equals("Success"))
                    {
                        foreach (var users in response.List)
                            list.Add(users);
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
