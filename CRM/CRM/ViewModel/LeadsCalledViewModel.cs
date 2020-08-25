using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model;
using CRM.Model.AdminModel;
using CRM.SuperAdmin.Model;
using CRM.View.Admin.Lead;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.ViewModel
{
    public class LeadsCalledViewModel : BaseViewModel
    {
        #region CTOR
        public LeadsCalledViewModel(INavigation navigation) : base(navigation)
        {
        }
        #endregion

        #region Commands
        public DelegateCommand Day1Command => new DelegateCommand(ExecuteOnDay1);
        public DelegateCommand Day7Command => new DelegateCommand(ExecuteOnDay7);
        public DelegateCommand Day30Command => new DelegateCommand(ExecuteOnDay30);
        public DelegateCommand ViewLeadCommand => new DelegateCommand(ExecuteOnViewLead);
        #endregion

        #region Properties
        private Color _day1Color;
        public Color Day1Color
        {
            get => _day1Color;
            set { _day1Color = value; OnPropertyChanged(); }
        }

        private Color _day7Color;
        public Color Day7Color
        {
            get => _day7Color;
            set { _day7Color = value; OnPropertyChanged(); }
        }

        private Color _day30Color;
        public Color Day30Color
        {
            get => _day30Color;
            set { _day30Color = value; OnPropertyChanged(); }
        }

        private ObservableCollection<LeadsCalledData> _leadsCalledData;
        public ObservableCollection<LeadsCalledData> LeadsCalledData
        {
            get => _leadsCalledData;
            set { _leadsCalledData = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public void Initialize(int count)
        {
            if (count == 1)
                ExecuteOnDay1(null);
            else if (count == 7)
                ExecuteOnDay7(null);
            else if (count == 30)
                ExecuteOnDay30(null);
        }
        public async void ExecuteOnDay1(object obj)
        {
            Day1Color = Color.FromHex("#2baae1");
            Day7Color = Day30Color = Color.FromHex("#4C4C4C");
            var fromDate = DateTime.Now.ToString("MM-dd-yyyy");
            var toDate = DateTime.Now.ToString("MM-dd-yyyy");
            LeadsCalledData = await GetLeadsCalledData(fromDate, toDate);
        }
        public async void ExecuteOnDay7(object obj)
        {
            Day7Color = Color.FromHex("#2baae1");
            Day1Color = Day30Color = Color.FromHex("#4C4C4C");
            var toDate = DateTime.Now.ToString("MM-dd-yyyy");
            var fromDate = DateTime.Now.AddDays(-7).ToString("MM-dd-yyyy");
            LeadsCalledData = await GetLeadsCalledData(fromDate, toDate);
        }
        public async void ExecuteOnDay30(object obj)
        {
            Day30Color = Color.FromHex("#2baae1");
            Day7Color = Day1Color = Color.FromHex("#4C4C4C");
            var toDate = DateTime.Now.ToString("MM-dd-yyyy");
            var fromDate = DateTime.Now.AddDays(-30).ToString("MM-dd-yyyy");
            LeadsCalledData = await GetLeadsCalledData(fromDate, toDate);
        }
        public async Task<ObservableCollection<LeadsCalledData>> GetLeadsCalledData(string fromDate, string toDate)
        {
            var list = new ObservableCollection<LeadsCalledData>();
            try
            {
                ShowLoading();
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    var userId = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.GetLeadsCalledListUrl, userId, fromDate, toDate), string.Empty);
                    var response = await apicall.Get<LeadsCalledModel>();
                    if (response != null && response.Result && response.List != null && response.Message.Equals("Success"))
                    {
                        foreach (var lead in response.List)
                            list.Add(lead);
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
        public async void ExecuteOnViewLead(object obj)
        {
            int count = 0;
            if (Day1Color == Color.FromHex("#2baae1"))
                count = 1;
            else if (Day7Color == Color.FromHex("#2baae1"))
                count = 7;
            else if (Day30Color == Color.FromHex("#2baae1"))
                count = 30;

            var userID = obj as LeadsCalledData;
            await _navigation.PushAsync(new LeadDetails(userID.UserId, count));
        }
        #endregion
    }
}
