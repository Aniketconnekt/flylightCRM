using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model;
using CRM.View.TransferLead;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.ViewModel
{
    public class UpdateLeadInfoViewModel : BaseViewModel
    {
        #region Data Members
        List<NewLeadsData> _newLeadsDatas = new List<NewLeadsData>();
        string _updateType = String.Empty;
        #endregion

        #region CTOR
        public UpdateLeadInfoViewModel(INavigation navigation) : base(navigation)
        {
           StartDate = InitializeDate();
        }
        #endregion

        #region Commands
        public DelegateCommand UpdateLeadCommand => new DelegateCommand(ExecuteOnUpdateLead);
        public DelegateCommand CancelCommand => new DelegateCommand(ExecuteOnCancel);
        public DelegateCommand TransferLeadCommand => new DelegateCommand(ExecuteOnTransferLead);
        #endregion

        #region Properties
        private AddUserLeadModel _addUserLeadModel = new AddUserLeadModel();
        public AddUserLeadModel AddUserLeadModel
        {
            get => _addUserLeadModel;
            set { _addUserLeadModel = value; OnPropertyChanged(); }
        }

        private int _id;
        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        public string _name;
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
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

        public StateData _state = new StateData();
        public StateData State
        {
            get => _state;
            set { _state = value; OnPropertyChanged(); }
        }

        private ObservableCollection<StateData> _stateCollection;
        public ObservableCollection<StateData> StateCollection
        {
            get { return _stateCollection; }
            set { _stateCollection = value; OnPropertyChanged(); }
        }

        public string _email;
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async void Initialize(List<NewLeadsData> newLeadsDatasList, string updateType)
        {
            _updateType = updateType;
            StateCollection = await Task.Run(async () => await GetStateList());
            _newLeadsDatas = newLeadsDatasList;
            var newLeadsData = newLeadsDatasList.FirstOrDefault();
            if (newLeadsData != null)
            {
                Id = newLeadsData.Id;
                Name = newLeadsData.Name;
                if (!string.IsNullOrWhiteSpace(newLeadsData.DOB))
                {
                    DOB = Convert.ToDateTime(newLeadsData.DOB).ToString("dd-MM-yyyy");
                    BindStartDate(Convert.ToDateTime(newLeadsData.DOB));
                }
                MobileNumber = newLeadsData.MobileNumber;
                AlternateNo = newLeadsData.AlternateNo;
                Company = newLeadsData.Company;
                Address = newLeadsData.Address;
                City = newLeadsData.City;
                District = newLeadsData.District;
                State = StateCollection.ToList().Find(s => s.Statename == newLeadsData.State);
                Email = newLeadsData.Email;
            }
        }
        public async void ExecuteOnUpdateLead(object obj)
        {
            try
            {
                if (await Validation())
                {
                    if (_updateType.Equals("UpdateUserLead"))
                        await UpdateLead();
                    else
                        await UpdateAllottedLead();
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
            if (string.IsNullOrWhiteSpace(Name))
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
        private async Task UpdateLead()
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
                    AddUserLeadModel.Id = Id;
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
                        await _navigation.PushAsync(new View.Successfull.SuccessfullPage());
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
        private async Task UpdateAllottedLead()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    ShowLoading();
                    string[] dobArr = DOB.Split('-');
                    var dob = dobArr[1] + "-" + dobArr[0] + "-" + dobArr[2];
                    HttpClientHelper apicall = new HttpClientHelper(ApiUrls.UpdateAlotedLeadByUserUrl, string.Empty);
                    AddUserLeadModel.Id = Id;
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
                    AddUserLeadModel.CreatedById = Settings.CRM_CreatedById; //await SecureStorage.GetAsync(AppConstant.CreatedById);
                    AddUserLeadModel.UserId = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    var json = JsonConvert.SerializeObject(AddUserLeadModel);
                    var response = await apicall.Post<LoginModel>(json);
                    if (response != null && response.Result && response.Object != null && response.Message.Equals("Success"))
                    {
                        HideLoading();
                        await _navigation.PushAsync(new View.Successfull.SuccessfullPage());
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
            Name = Company = MobileNumber = AlternateNo = Address = City = District = Email = String.Empty;
            State = new StateData();
            AddUserLeadModel = new AddUserLeadModel();
        }
        public void ExecuteOnCancel(object obj)
        {
            Application.Current.MainPage = new NavigationPage(new View.Menu.MasterDetailView())
            {
                BarBackgroundColor = Color.FromHex("#2699ca"),
                BarTextColor = Color.FromHex("#FFFFFF"),
            };
        }
        public void ExecuteOnTransferLead(object obj)
        {
            App.MasterDetailPage.Detail = new NavigationPage(new TransferLeadPage(_newLeadsDatas))
            {
                BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                BarTextColor = Color.FromHex(App.nav_bar_text_color),
            };
        }
        private void BindStartDate(DateTime dt)
        {
            ObservableCollection<object> todaycollection = new ObservableCollection<object>();

            if (dt.Date.Day < 10)
                todaycollection.Add("0" + dt.Date.Day);
            else
                todaycollection.Add(dt.Date.Day.ToString());

            if (dt.Date.Month < 10)
                todaycollection.Add("0" + dt.Date.Month);
            else
                todaycollection.Add(dt.Date.Month.ToString());

            todaycollection.Add(dt.Date.Year.ToString());

            StartDate = todaycollection;
        }
        #endregion
    }
}
