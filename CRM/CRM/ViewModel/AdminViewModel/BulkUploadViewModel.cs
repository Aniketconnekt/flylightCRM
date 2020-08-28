using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model.AdminModel;
using CRM.View.Admin.Lead;
using Plugin.FilePicker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.ViewModel.AdminViewModel
{
    public class BulkUploadViewModel : BaseViewModel
    {
        private byte[] _fileDataBytes = null;
        private string _fileName = String.Empty;

        #region CTOR
        public BulkUploadViewModel(INavigation navigation) : base(navigation)
        {
        }
        #endregion

        #region Commands
        public DelegateCommand SubmitCommand => new DelegateCommand(ExecuteOnSubmit);
        public DelegateCommand UploadFileCommand => new DelegateCommand(ExecuteOnUploadFile);
        public DelegateCommand DownloadFileCommand => new DelegateCommand(ExecuteOnDownloadFile);
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

        private bool _isUploadBtnEnabled = false;
        public bool IsUploadBtnEnabled
        {
            get => _isUploadBtnEnabled;
            set { _isUploadBtnEnabled = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async void Initialize()
        {
            ShowLoading();
            var data = await Task.Run(async () => await GetCampaignData());
            CampaignList = data.ToList();
        }
        public async void ExecuteOnUploadFile(object obj)
        {
            try
            {
                var fileTypes = new string[] { ".xls", ".xlsx" };
                var pickedFile = await CrossFilePicker.Current.PickFile(fileTypes);
                if (pickedFile != null)
                {
                    var extension = Path.GetExtension(pickedFile.FileName);
                    if (fileTypes.Any(s => s == extension))
                    {
                        _fileDataBytes = pickedFile.DataArray;
                        _fileName = pickedFile.FileName;
                        IsUploadBtnEnabled = true;
                    }
                    else
                        await ShowAlert("Please select execel file only.");
                }
            }
            catch (Exception ex)
            {
            }
        }
        public async void ExecuteOnSubmit(object obj)
        {
            try
            {
                if (SelectedCampaign == null)
                {
                    await ShowAlert("Please select campaign.");
                    return;
                }
                else if (_fileDataBytes == null)
                {
                    await ShowAlert("Please choose a file.");
                    return;
                }

                ShowLoading();
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    var userId = Settings.CRM_UserId; //await SecureStorage.GetAsync(AppConstant.UserId);
                    var auth = SelectedCampaign.Id + "," + userId;
                    HttpClientHelper apicall = new HttpClientHelper(ApiUrls.LeadBulkUploadUrl, auth);
                    var response = await apicall.PostAsync<string>(_fileDataBytes, _fileName);
                    if (response=="Success")
                    {
                        HideLoading();
                        await ShowAlert("Lead uploaded successfully.");
                        await _navigation.PushAsync(new Leads());
                    }
                    else
                    {
                        HideLoading();
                        await ShowAlert(response);
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
        public async void ExecuteOnDownloadFile(object obj)
        {
            Uri uri = new Uri("http://flylight.connekt.in/Content/Template/BulkUploadTemplate.xlsx");
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        #endregion
    }
}
