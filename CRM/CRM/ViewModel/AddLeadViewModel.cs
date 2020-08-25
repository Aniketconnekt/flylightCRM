using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model;
using CRM.View.LeadSelection;
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
    public class AddLeadViewModel : BaseViewModel
    {
        #region CTOR
        public AddLeadViewModel(INavigation navigation) : base(navigation)
        {
            StartDate = InitializeDate();
        }
        #endregion

        #region Commands
        public DelegateCommand AddLeadCommand => new DelegateCommand(ExecuteOnAddLead);
        #endregion

        #region Properties
        private List<NewCampaignData> _campaignList;
        public List<NewCampaignData> CampaignList
        {
            get => _campaignList;
            set { _campaignList = value; OnPropertyChanged(); }
        }

        private NewCampaignData _selectedCampaign;
        public NewCampaignData SelectedCampaign
        {
            get => _selectedCampaign;
            set { _selectedCampaign = value; OnPropertyChanged(); }
        }

        private AddUserLeadModel _addUserLeadModel = new AddUserLeadModel();
        public AddUserLeadModel AddUserLeadModel
        {
            get => _addUserLeadModel;
            set { _addUserLeadModel = value; OnPropertyChanged(); }
        }

        public string _name;
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public string _dOB = "dd-MM-yyyy";
        public string DOB
        {
            get => _dOB;
            set { _dOB = value; OnPropertyChanged(); }
        }

        private ObservableCollection<object> _startdate;
        public ObservableCollection<object> StartDate
        {
            get { return _startdate; }
            set
            {
                _startdate = value;
                OnPropertyChanged("StartDate");
                DOB = _startdate.GetDateTime();
            }
        }

        public string _company;
        public string Company
        {
            get => _company;
            set { _company = value; OnPropertyChanged(); }
        }

        public string _mobileNumber;
        public string MobileNumber
        {
            get => _mobileNumber;
            set { _mobileNumber = value; OnPropertyChanged(); }
        }

        public string _alternateNo;
        public string AlternateNo
        {
            get => _alternateNo;
            set { _alternateNo = value; OnPropertyChanged(); }
        }

        private string _address;
        public string Address
        {
            get => _address;
            set { _address = value; OnPropertyChanged(); }
        }

        public string _city;
        public string City
        {
            get => _city;
            set { _city = value; OnPropertyChanged(); }
        }

        public string _district;
        public string District
        {
            get => _district;
            set { _district = value; OnPropertyChanged(); }
        }

        private ObservableCollection<StateData> _stateCollection;
        public ObservableCollection<StateData> StateCollection
        {
            get { return _stateCollection; }
            set { _stateCollection = value; OnPropertyChanged(); }
        }

        public StateData _state = new StateData();
        public StateData State
        {
            get => _state;
            set { _state = value; OnPropertyChanged(); }
        }

        public string _email;
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async void Initialize()
        {
            ShowLoading();
            var data = await Task.Run(async () => await GetCampaignList());
            CampaignList = data.ToList();
            StateCollection = await Task.Run(async () => await GetStateList());
        }
        public async void ExecuteOnAddLead(object obj)
        {
            try
            {
                if (await Validation())
                    await AddLead();
            }
            catch (Exception ex)
            {
                HideLoading();
                await ShowAlert(ex.Message);
            }
        }
        private async Task<bool> Validation()
        {
            if(SelectedCampaign == null)
            {
                await ShowAlert("Please select campaign.");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(Name))
            {
                await ShowAlert("Please enter name.");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(Company))
            {
                await ShowAlert("Please enter company.");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(MobileNumber))
            {
                await ShowAlert("Please enter mobile number.");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(Address))
            {
                await ShowAlert("Please enter address.");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(City))
            {
                await ShowAlert("Please enter city.");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(District))
            {
                await ShowAlert("Please enter district.");
                return false;
            }
            else if (State == null)
            {
                await ShowAlert("Please enter state.");
                return false;
            }
            else if (!string.IsNullOrWhiteSpace(Email) && !Helper.IsEmailValid(Email))
            {
                await ShowAlert("Please enter valid email.");
                return false;
            }

            return true;
        }
        private async Task AddLead()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    ShowLoading();
                    string[] dobArr = DOB.Split('-');
                    var dob = dobArr[1] + "-" + dobArr[0] + "-" + dobArr[2];
                    HttpClientHelper apicall = new HttpClientHelper(ApiUrls.AddUpdateLeadUserUrl, string.Empty);
                    AddUserLeadModel.CampaignId = SelectedCampaign.Id;
                    AddUserLeadModel.Name = Name;
                    AddUserLeadModel.DOB = dob;
                    AddUserLeadModel.MobileNumber = MobileNumber;
                    AddUserLeadModel.AlternateNumber = AlternateNo;
                    AddUserLeadModel.Company = Company;
                    AddUserLeadModel.Address = Address;
                    AddUserLeadModel.City = City;
                    AddUserLeadModel.District = District;
                    AddUserLeadModel.State = State.Statename;
                    AddUserLeadModel.Email = Email;
                    AddUserLeadModel.CreatedById = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    var json = JsonConvert.SerializeObject(AddUserLeadModel);
                    var response = await apicall.Post<LoginModel>(json);
                    if (response != null && response.Result && response.Object != null && response.Message.Equals("Success"))
                    {
                        HideLoading();
                        await _navigation.PushAsync(new LeadSelectionPage(null));
                        ObjectReset();
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
        public void ObjectReset()
        {
            SelectedCampaign = null;
            Name = Company = MobileNumber = AlternateNo = Address = City = District = Email = String.Empty;
            AddUserLeadModel = new AddUserLeadModel();
            State = new StateData();
        }
        #endregion
    }
}
