using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model;
using CRM.View.LeadSelection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.ViewModel
{
    class SearchLeadViewModel : BaseViewModel
    {
        #region CTOR
        public SearchLeadViewModel(INavigation navigation) : base(navigation)
        {
        }
        #endregion

        #region Commands
        public DelegateCommand SearchCommand => new DelegateCommand(ExecuteOnSearch);
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

        private List<CampaignActionData> _statusList;
        public List<CampaignActionData> StatusList
        {
            get => _statusList;
            set { _statusList = value; OnPropertyChanged(); }
        }

        private CampaignActionData _selectedStatus;
        public CampaignActionData SelectedStatus
        {
            get => _selectedStatus;
            set { _selectedStatus = value; OnPropertyChanged(); }
        }

        private string _leadName;
        public string LeadName
        {
            get => _leadName;
            set { _leadName = value; OnPropertyChanged(); }
        }

        private string _mobileNumber;
        public string MobileNumber
        {
            get => _mobileNumber;
            set { _mobileNumber = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async Task Initialize()
        {
            ShowLoading();
            var data = await Task.Run(async () => await GetCampaignList());
            CampaignList = data.ToList();
            var statusData = await Task.Run(async () => await GetLadActionList());
            StatusList = statusData.ToList();
        }
        public async void ExecuteOnSearch(object obj)
        {
            try
            {
                if(SelectedStatus == null && SelectedCampaign == null && string.IsNullOrWhiteSpace(LeadName) 
                    && string.IsNullOrWhiteSpace(MobileNumber))
                {
                    await ShowAlert("Please fill atleast one field.");
                    return;
                }

                int statusId = 0;
                int campaginId = 0;
                if (SelectedStatus != null)
                    statusId = SelectedStatus.Id;
                if (SelectedCampaign != null)
                    campaginId = SelectedCampaign.Id;

                var searchData = new Tuple<int, int, string, string>(statusId, campaginId, LeadName, MobileNumber);
                await _navigation.PushAsync(new LeadSelectionPage(searchData));
            }
            catch (Exception ex)
            {
            }
        }
        #endregion
    }
}
