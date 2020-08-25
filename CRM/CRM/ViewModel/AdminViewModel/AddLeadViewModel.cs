using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model;
using CRM.Model.AdminModel;
using CRM.View.Admin.Lead;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.ViewModel.AdminViewModel
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
        public DelegateCommand ContactsCommand => new DelegateCommand(ExecuteOnContacts);
        #endregion

        #region Properties
        private List<CampaignData> _campaignList;
        public List<CampaignData> CampaignList
        {
            get => _campaignList;
            set { _campaignList = value; OnPropertyChanged(); }
        }

        private CampaignData _selectedCampaign;
        public CampaignData SelectedCampaign
        {
            get => _selectedCampaign;
            set { _selectedCampaign = value; OnPropertyChanged(); }
        }

        private AddLeadModel _addLeadModel = new AddLeadModel();
        public AddLeadModel AddLeadModel
        {
            get => _addLeadModel;
            set
            {
                _addLeadModel
= value; OnPropertyChanged();
            }
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

        public string _gSTNO;
        public string GSTNO
        {
            get => _gSTNO;
            set { _gSTNO = value; OnPropertyChanged(); }
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

        private string _btnText;
        public string BtnText
        {
            get => _btnText;
            set { _btnText = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async void Initialize(LeadsData leadsData)
        {
            ShowLoading();
            var data = await Task.Run(async () => await GetCampaignData());
            CampaignList = data.ToList();

            StateCollection = await Task.Run(async () => await GetStateList());

            if (leadsData != null)
            {
                Id = leadsData.Id;
                SelectedCampaign = CampaignList.Find(f => f.Id == leadsData.CampaignId);
                Name = leadsData.Name;
                if (!string.IsNullOrWhiteSpace(leadsData.DOB))
                {
                    DOB = Convert.ToDateTime(leadsData.DOB).ToString("dd-MM-yyyy");
                    BindStartDate(Convert.ToDateTime(leadsData.DOB));
                }
                MobileNumber = leadsData.MobileNumber;
                AlternateNo = leadsData.AlternateNo;
                Company = leadsData.Company;
                GSTNO = leadsData.GSTNO;
                Address = leadsData.Address;
                City = leadsData.City;
                District = leadsData.District;
                State = StateCollection.ToList().Find(s => s.Statename == leadsData.State);
                Email = leadsData.Email;
                BtnText = "Update";
            }
            else
                BtnText = "Add";
        }
        public async void Initialize(ContactModel contactModel)
        {
            ShowLoading();
            var data = await Task.Run(async () => await GetCampaignData());
            CampaignList = data.ToList();

            StateCollection = await Task.Run(async () => await GetStateList());

            if (contactModel != null)
            {
                Name = contactModel.Name;
                MobileNumber = contactModel.PhoneNumbers.FirstOrDefault();
                //if (contactModel.PhoneNumbers.Count() > 1)
                //    AlternateNo = contactModel.PhoneNumbers.LastOrDefault();
                Email = contactModel.Emails.FirstOrDefault();
                BtnText = "Add";
            }
        }
        public async void ExecuteOnAddLead(object obj)
        {
            try
            {
                if (await Validation())
                    await AddUpdateLead();
            }
            catch (Exception ex)
            {
                HideLoading();
                await ShowAlert(ex.Message);
            }
        }
        private async Task<bool> Validation()
        {
            if (SelectedCampaign == null)
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
            else if (!string.IsNullOrWhiteSpace(GSTNO) && !Helper.IsGSTPanAadhaarValid(GSTNO))
            {
                await ShowAlert("Please enter valid GST.");
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
        private async Task AddUpdateLead()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    ShowLoading();
                    string[] dobArr = DOB.Split('-');
                    var dob = dobArr[1] + "-" + dobArr[0] + "-" + dobArr[2];
                    HttpClientHelper apicall = new HttpClientHelper(ApiUrls.AddUpdateLeadUrl, string.Empty);
                    AddLeadModel.CampaignId = SelectedCampaign.Id;
                    AddLeadModel.Id = Id;
                    AddLeadModel.Name = Name;
                    AddLeadModel.DOB = dob;
                    AddLeadModel.MobileNumber = MobileNumber;
                    AddLeadModel.AlternateNo = AlternateNo;
                    AddLeadModel.Company = Company;
                    AddLeadModel.GSTNO = GSTNO;
                    AddLeadModel.Address = Address;
                    AddLeadModel.City = City;
                    AddLeadModel.District = District;
                    AddLeadModel.State = State.Statename;
                    AddLeadModel.Email = Email;
                    AddLeadModel.CreatedById = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    var json = JsonConvert.SerializeObject(AddLeadModel);
                    var response = await apicall.Post<LoginModel>(json);
                    if (response != null && response.Result && response.Object != null && response.Message.Equals("Success"))
                    {
                        HideLoading();
                        if (BtnText.Equals("Add"))
                            await ShowAlert("Lead added successfully.");
                        else
                            await ShowAlert("Lead updated successfully.");
                        await _navigation.PushAsync(new View.Admin.Lead.Leads());
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
            Id = 0;
            Name = Company = GSTNO = MobileNumber = AlternateNo = Address = City = District = DOB = Email = String.Empty;
            State = new StateData();
            BtnText = "Add";
            AddLeadModel = new AddLeadModel();
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
        public async void ExecuteOnContacts(object obj)
        {
            await _navigation.PushAsync(new ContactsPage());
        }
        #endregion
    }
}
