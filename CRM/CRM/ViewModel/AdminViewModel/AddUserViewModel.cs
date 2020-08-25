using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model;
using CRM.SuperAdmin.Model;
using CRM.View.Admin.Users;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.ViewModel.AdminViewModel
{
    public class AddUserViewModel : BaseViewModel
    {
        #region CTOR
        public AddUserViewModel(INavigation navigation) : base(navigation)
        {
            StartDate = InitializeDate();
        }
        #endregion

        #region Commands
        public DelegateCommand SaveAdminCustomerCommand => new DelegateCommand(ExecuteOnSaveAdminCustomer);
        #endregion

        #region Properties
        private AddAdminCustomerModel _addAdminCustomerModel = new AddAdminCustomerModel();
        public AddAdminCustomerModel AddAdminCustomerModel
        {
            get => _addAdminCustomerModel;
            set { _addAdminCustomerModel = value; OnPropertyChanged(); }
        }

        private int _id;
        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        public string _userName;
        public string UserName
        {
            get => _userName;
            set { _userName = value; OnPropertyChanged(); }
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

        public string _company;
        public string Company
        {
            get => _company;
            set { _company = value; OnPropertyChanged(); }
        }

        public string _gSTNo;
        public string GSTNo
        {
            get => _gSTNo;
            set { _gSTNo = value; OnPropertyChanged(); }
        }

        private string _address;
        public string Address
        {
            get => _address;
            set { _address = value; OnPropertyChanged(); }
        }

        public string _cityName;
        public string CityName
        {
            get => _cityName;
            set { _cityName = value; OnPropertyChanged(); }
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

        public StateData _stateName = new StateData();
        public StateData StateName
        {
            get => _stateName;
            set { _stateName = value; OnPropertyChanged(); }
        }

        public string _email;
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public string _password;
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public string _createdById;
        public string CreatedById
        {
            get => _createdById;
            set { _createdById = value; OnPropertyChanged(); }
        }

        private string _btnText;
        public string BtnText
        {
            get => _btnText;
            set { _btnText = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async void Initialize(UsersData usersData)
        {
            StateCollection = await Task.Run(async () => await GetStateList());
            if (usersData != null)
            {
                Id = usersData.Id;
                UserName = usersData.UserName;
                if (!string.IsNullOrWhiteSpace(usersData.DOB))
                {
                    DOB = Convert.ToDateTime(usersData.DOB).ToString("dd-MM-yyyy");
                    BindStartDate(Convert.ToDateTime(usersData.DOB));
                }
                MobileNumber = usersData.MobileNumber;
                AlternateNo = usersData.AlternateNo;
                Company = usersData.Company;
                GSTNo = usersData.GSTNo;
                Address = usersData.Address;
                CityName = usersData.CityName;
                District = usersData.District;
                StateName = StateCollection.ToList().Find(s => s.Statename == usersData.StateName);
                Email = usersData.Email;
                Password = usersData.Password;
                CreatedById = usersData.CreatedById;
                BtnText = "Update";
            }
            else
                BtnText = "Add";
        }
        public async void ExecuteOnSaveAdminCustomer(object obj)
        {
            try
            {
                if (await Validation())
                {
                    if (BtnText.Equals("Add"))
                        await AddAdminCustomer();
                    else
                        await UpdateAdminCustomer();
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
            if (string.IsNullOrWhiteSpace(UserName))
            {
                await ShowAlert("Please enter username.");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(MobileNumber))
            {
                await ShowAlert("Please enter mobile number.");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(Company))
            {
                await ShowAlert("Please enter company.");
                return false;
            }
            else if (!string.IsNullOrWhiteSpace(GSTNo) && !Helper.IsGSTPanAadhaarValid(GSTNo))
            {
                await ShowAlert("Please enter valid GST.");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(Address))
            {
                await ShowAlert("Please enter address.");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(CityName))
            {
                await ShowAlert("Please enter city.");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(District))
            {
                await ShowAlert("Please enter district.");
                return false;
            }
            else if (StateName == null)
            {
                await ShowAlert("Please enter state.");
                return false;
            }
            else if (!string.IsNullOrWhiteSpace(Email) && !Helper.IsEmailValid(Email))
            {
                await ShowAlert("Please enter valid email.");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(Password))
            {
                await ShowAlert("Please enter password.");
                return false;
            }

            return true;
        }
        private async Task AddAdminCustomer()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    ShowLoading();
                    string[] dobArr = DOB.Split('-');
                    var dob = dobArr[1] + "-" + dobArr[0] + "-" + dobArr[2];
                    HttpClientHelper apicall = new HttpClientHelper(ApiUrls.SaveUserUrl, string.Empty);
                    AddAdminCustomerModel.UserName = UserName;
                    AddAdminCustomerModel.DOB = dob;
                    AddAdminCustomerModel.MobileNumber = MobileNumber;
                    AddAdminCustomerModel.AlternateNo = AlternateNo;
                    AddAdminCustomerModel.Company = Company;
                    AddAdminCustomerModel.GSTNo = GSTNo;
                    AddAdminCustomerModel.Address = Address;
                    AddAdminCustomerModel.CityId = 1;
                    AddAdminCustomerModel.CityName = CityName;
                    AddAdminCustomerModel.District = District;
                    AddAdminCustomerModel.StateId = StateName.Id;
                    AddAdminCustomerModel.StateName = StateName.Statename;
                    AddAdminCustomerModel.Email = Email;
                    AddAdminCustomerModel.Password = Password;
                    AddAdminCustomerModel.CreatedById = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    var json = JsonConvert.SerializeObject(AddAdminCustomerModel);
                    var response = await apicall.Post<LoginModel>(json);
                    if (response != null && response.Result && response.Object != null && response.Message.Equals("Success"))
                    {
                        HideLoading();
                        await ShowAlert("New user added successfully.");
                        await _navigation.PushAsync(new AddUserSelectCampaign());
                        AddAdminCustomerModel = new AddAdminCustomerModel();
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
        private async Task UpdateAdminCustomer()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    ShowLoading();
                    string[] dobArr = DOB.Split('-');
                    var dob = dobArr[1] + "-" + dobArr[0] + "-" + dobArr[2];
                    HttpClientHelper apicall = new HttpClientHelper(ApiUrls.UpdateUserUrl, string.Empty);
                    AddAdminCustomerModel.Id = Id;
                    AddAdminCustomerModel.UserName = UserName;
                    AddAdminCustomerModel.DOB = dob;
                    AddAdminCustomerModel.MobileNumber = MobileNumber;
                    AddAdminCustomerModel.AlternateNo = AlternateNo;
                    AddAdminCustomerModel.Company = Company;
                    AddAdminCustomerModel.GSTNo = GSTNo;
                    AddAdminCustomerModel.Address = Address;
                    AddAdminCustomerModel.CityId = 1;
                    AddAdminCustomerModel.CityName = CityName;
                    AddAdminCustomerModel.District = District;
                    AddAdminCustomerModel.StateId = StateName.Id;
                    AddAdminCustomerModel.StateName = StateName.Statename;
                    AddAdminCustomerModel.Email = Email;
                    AddAdminCustomerModel.Password = Password;
                    var json = JsonConvert.SerializeObject(AddAdminCustomerModel);
                    var response = await apicall.Post<LoginModel>(json);
                    if (response != null && response.Result && response.Object != null && response.Message.Equals("Success"))
                    {
                        HideLoading();
                        await ShowAlert("User updated successfully.");
                        await _navigation.PushAsync(new AddUserSelectCampaign());
                        AddAdminCustomerModel = new AddAdminCustomerModel();
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
