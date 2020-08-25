using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model;
using CRM.View.CreditForm;
using CRM.View.Menu;
using CRM.View.SendReminder;
using CRM.View.TransferLead;
using CRM.View.UpdateInformation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.ViewModel
{
    public class UpdateInformationViewModel : BaseViewModel
    {
        #region CTOR
        public UpdateInformationViewModel(INavigation navigation) : base(navigation)
        {
            StartDate = InitializeDate();
        }
        #endregion

        #region Commands
        public DelegateCommand LeadTransferCommand => new DelegateCommand(ExecuteOnLeadTransfer);
        public DelegateCommand CreditFormCommand => new DelegateCommand(ExecuteOnCreditForm);
        public DelegateCommand SendReminderCommand => new DelegateCommand(ExecuteOnSendReminder);
        public DelegateCommand EditLeadDetailCommand => new DelegateCommand(ExecuteOnEditLeadDetail);
        public DelegateCommand SaveCommand => new DelegateCommand(ExecuteOnSave);
        public DelegateCommand CancelCommand => new DelegateCommand(ExecuteOnCancel);
        #endregion

        #region Properties
        private NewLeadsData _newLeadsData;
        public NewLeadsData NewLeadsData
        {
            get => _newLeadsData;
            set { _newLeadsData = value; OnPropertyChanged(); }
        }

        private List<CampaignActionData> _actionsList;
        public List<CampaignActionData> ActionsList
        {
            get => _actionsList;
            set { _actionsList = value; OnPropertyChanged(); }
        }

        private CampaignActionData _selectedAction;
        public CampaignActionData SelectedAction
        {
            get => _selectedAction;
            set { _selectedAction = value; OnPropertyChanged(); }
        }

        private List<string> _statusList;
        public List<string> StatusList
        {
            get => _statusList;
            set { _statusList = value; OnPropertyChanged(); }
        }

        private string _selectedStatus;
        public string SelectedStatus
        {
            get => _selectedStatus;
            set { _selectedStatus = value; OnPropertyChanged(); }
        }

        private string _actionDate = "dd-MM-yyyy";
        public string ActionDate
        {
            get => _actionDate;
            set { _actionDate = value; OnPropertyChanged(); }
        }

        private ObservableCollection<object> _startdate;
        public ObservableCollection<object> StartDate
        {
            get { return _startdate; }
            set
            {
                _startdate = value;
                OnPropertyChanged("StartDate");
                ActionDate = _startdate.GetDateTime();
            }
        }

        private TimeSpan _followupTime = new TimeSpan(DateTime.Now.Hour,DateTime.Now.Minute,DateTime.Now.Second);
        public TimeSpan FollowupTime
        {
            get => _followupTime;
            set { _followupTime = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async void Initialize(NewLeadsData newLeadsData)
        {
            NewLeadsData = newLeadsData;

            var actionData = await Task.Run(async () => await GetLadActionList());
            ActionsList = actionData.ToList();

            var statusData = new List<string>() { "Called", "UnCalled", "FollowUp"};
            StatusList = statusData;
            SelectedStatus = StatusList.FirstOrDefault();

            SelectedAction = ActionsList.Find(a => a.Id == newLeadsData.ActionId);
            if (!string.IsNullOrEmpty(NewLeadsData.Status))
                SelectedStatus = StatusList.Find(s => s == NewLeadsData.Status);
        }
        public void ExecuteOnLeadTransfer(object obj)
        {
            var list = new List<NewLeadsData>();
            list.Add(NewLeadsData);
            App.MasterDetailPage.Detail = new NavigationPage(new TransferLeadPage(list))
            {
                BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                BarTextColor = Color.FromHex(App.nav_bar_text_color),
            };
        }
        public async void ExecuteOnCreditForm(object obj)
        {
            await _navigation.PushAsync(new CreditFormPage(NewLeadsData));
        }
        public async void ExecuteOnSendReminder(object obj)
        {
            await _navigation.PushAsync(new SendReminderPage(NewLeadsData));
        }
        public async void ExecuteOnSave(object obj)
        {
            try
            {
                if(await Validation())
                {
                    var current = Connectivity.NetworkAccess;
                    if (current == NetworkAccess.Internet)
                    {
                        ShowLoading();
                        string[] dobArr = ActionDate.Split('-');
                        var actionDate = dobArr[1] + "-" + dobArr[0] + "-" + dobArr[2];
                        HttpClientHelper apicall = new HttpClientHelper(ApiUrls.UpdateAllotedLeadUrl, string.Empty);
                        NewLeadsData.ActionDate = actionDate;
                        NewLeadsData.FollowupTime = FollowupTime;
                        NewLeadsData.ActionId = SelectedAction.Id;
                        NewLeadsData.Status = SelectedStatus;
                        //NewLeadsData.UserId = await SecureStorage.GetAsync(AppConstant.UserId);
                        NewLeadsData.CreatedById = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                        var json = JsonConvert.SerializeObject(NewLeadsData);
                        var response = await apicall.Post<LoginModel>(json);
                        if (response != null && response.Result && response.Object != null && response.Message.Equals("Success"))
                        {
                            HideLoading();
                            await _navigation.PushAsync(new View.Successfull.SuccessfullPage());
                            NewLeadsData = new NewLeadsData();
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
            }
            catch (Exception ex)
            {
                HideLoading();
                await ShowAlert(ex.Message);
            }
        }
        private async Task<bool> Validation()
        {
            if (SelectedAction == null)
            {
                await ShowAlert("Please enter action.");
                return false;
            }
            else if (SelectedStatus == null)
            {
                await ShowAlert("Please enter status.");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(NewLeadsData.Description))
            {
                await ShowAlert("Please enter description.");
                return false;
            }

            return true;
        }
        public void ExecuteOnCancel(object obj)
        {
            Application.Current.MainPage = new NavigationPage(new MasterDetailView())
            {
                BarBackgroundColor = Color.FromHex("#2699ca"),
                BarTextColor = Color.FromHex("#FFFFFF"),
            };
        }
        public void ExecuteOnEditLeadDetail(object obj)
        {
            var list = new List<NewLeadsData>();
            list.Add(NewLeadsData);
            App.MasterDetailPage.Detail = new NavigationPage(new UpdateInformationPage(list, "UpdateAlotedLead"))
            {
                BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                BarTextColor = Color.FromHex(App.nav_bar_text_color),
            };
        }
        #endregion
    }
}
