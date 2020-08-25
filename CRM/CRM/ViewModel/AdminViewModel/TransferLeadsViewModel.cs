using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model.AdminModel;
using CRM.SuperAdmin.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.ViewModel.AdminViewModel
{
    public class TransferLeadsViewModel : BaseViewModel
    {
        #region CTOR
        public TransferLeadsViewModel(INavigation navigation) : base(navigation)
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

        private UsersData _selectedUsers;
        public UsersData SelectedUsers
        {
            get => _selectedUsers;
            set { _selectedUsers = value; OnPropertyChanged(); }
        }

        private ObservableCollection<LeadDetailsList> _transferList;
        public ObservableCollection<LeadDetailsList> TransferList
        {
            get => _transferList;
            set { _transferList = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async void Initialize(List<LeadDetailsList> leadDetailsLists, int userID)
        {
            ShowLoading();
            TransferList = new ObservableCollection<LeadDetailsList>(leadDetailsLists);
            var userData = await Task.Run(async () => await GetUsersData());
            UsersList = userData.ToList();
            SelectedUsers = UsersList.Find(u => u.Id == userID);
        }
        public async void ExecuteOnTransfer(object obj)
        {
            try
            {
                ShowLoading();
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    string selectedLeads = String.Empty;
                    foreach (var lead in TransferList)
                        selectedLeads += lead.LeadId.ToString() + ",";

                    selectedLeads = selectedLeads.Remove(selectedLeads.Length - 1, 1);

                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.TransferLeadUrl, selectedLeads, SelectedUsers.Id), string.Empty);
                    var response = await apicall.Get<bool>();
                    if (response)
                    {
                        HideLoading();
                        await _navigation.PushModalAsync(new View.Admin.Successful.Successful());
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
