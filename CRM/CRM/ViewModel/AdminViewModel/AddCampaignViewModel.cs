using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model.AdminModel;
using CRM.View.Admin.Compaign;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.ViewModel.AdminViewModel
{
    public class AddCampaignViewModel : BaseViewModel
    {
        #region CTOR
        public AddCampaignViewModel(INavigation navigation) : base(navigation)
        {
        }
        #endregion

        #region Commands
        public DelegateCommand SaveCampaignCommand => new DelegateCommand(ExecuteOnSaveCampaign);
        #endregion

        #region Properties
        private AddCampaignModel _addCampaignModel = new AddCampaignModel();
        public AddCampaignModel AddCampaignModel
        {
            get => _addCampaignModel;
            set { _addCampaignModel = value; OnPropertyChanged(); }
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

        public string _description;
        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
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
        public async void Initialize(CampaignData campaignData)
        {
            if (campaignData != null)
            {
                Id = campaignData.Id;
                Name = campaignData.Campaign;
                Description = campaignData.Description;
                CreatedById = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                BtnText = "Update";
            }
            else
                BtnText = "Add";
        }
        public async void ExecuteOnSaveCampaign(object obj)
        {
            try
            {
                if (await Validation())
                    await AddCampaign();
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
            //else if (string.IsNullOrWhiteSpace(Description))
            //{
            //    await ShowAlert("Please enter description.");
            //    return false;
            //}
            return true;
        }
        private async Task AddCampaign()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    ShowLoading();
                    HttpClientHelper apicall = new HttpClientHelper(ApiUrls.AddNewCampaignUrl, string.Empty);
                    AddCampaignModel.Id = Id;
                    AddCampaignModel.Name = Name;
                    AddCampaignModel.Description = Description;
                    AddCampaignModel.CreatedById = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    var json = JsonConvert.SerializeObject(AddCampaignModel);
                    var response = await apicall.Post<AddCampaignResponse>(json);
                    if (response != null && response.Result && response.Object != null && response.Message.Equals("Success"))
                    {
                        HideLoading();
                        if (BtnText.Equals("Add"))
                            await ShowAlert("Campaign added successfully.");
                        else
                            await ShowAlert("Campaign update successfully.");

                        App.MasterDetailPage.Detail = new NavigationPage(new Campaigns())
                        {
                            BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                            BarTextColor = Color.FromHex(App.nav_bar_text_color),
                        };

                        AddCampaignModel = new AddCampaignModel();
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
        #endregion
    }
}
