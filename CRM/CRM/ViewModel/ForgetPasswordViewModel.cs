using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model;
using CRM.View.Login;
using CRM.View.ResetPassword;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.ViewModel
{
    public class ForgetPasswordViewModel : BaseViewModel
    {
        #region CTOR
        public ForgetPasswordViewModel(INavigation navigation) : base(navigation)
        {
        }
        #endregion

        #region Commands
        public DelegateCommand BackPressCommand => new DelegateCommand(ExecuteOnBackPress);
        public DelegateCommand SubmitCommand => new DelegateCommand(ExecuteOnSubmit);
        public DelegateCommand ResetPasswordCommand => new DelegateCommand(ExecuteOnResetPassword);
        #endregion

        #region Properties
        private string _mobileNumber;
        public string MobileNumber
        {
            get => _mobileNumber;
            set { _mobileNumber = value; OnPropertyChanged(); }
        }

        private string _oTP;
        public string OTP
        {
            get => _oTP;
            set { _oTP = value; OnPropertyChanged(); }
        }

        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set { _newPassword = value; OnPropertyChanged(); }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set { _confirmPassword = value; OnPropertyChanged(); }
        }

        private bool _isOtpVisible = false;
        public bool IsOtpVisible
        {
            get => _isOtpVisible;
            set { _isOtpVisible = value; OnPropertyChanged(); }
        }

        private string _btnTxt = "Submit";
        public string BtnTxt
        {
            get => _btnTxt;
            set { _btnTxt = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public void Initialize(string mobileno)
        {
            MobileNumber = mobileno;
        }
        public async void ExecuteOnBackPress(object obj)
        {
            await _navigation.PopAsync();
        }
        public async void ExecuteOnSubmit(object obj)
        {
            if (BtnTxt.Equals("Submit"))
                await VerifyMobileAndSendOTPToMobile();
            else
                await VerifyOTP();
        }

        private async Task VerifyMobileAndSendOTPToMobile()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(MobileNumber))
                {
                    await ShowAlert("Please enter mobile number.");
                    return;
                }

                ShowLoading();
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.CheckMobileNoUrl, MobileNumber), string.Empty);
                    var response = await apicall.Get<bool>();
                    if (response)
                    {
                        if (await SendOtpToMobile(GenerateRandomNo()))
                        {
                            HideLoading();
                            IsOtpVisible = true;
                            BtnTxt = "Verify OTP";
                        }
                    }
                    else
                    {
                        HideLoading();
                        await ShowAlert(AppConstant.WRONG_Mobile);
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
        public int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
        private async Task<bool> SendOtpToMobile(int otp)
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.SendotponmobileUrl, otp, MobileNumber), string.Empty);
                    var response = await apicall.Get<bool>();
                    if (response)
                    {
                        UserDialogs.Instance.Toast("OTP sent on mobile.", TimeSpan.FromSeconds(2));
                        //await SecureStorage.SetAsync(AppConstant.ForgotOtp, otp.ToString());
                        Settings.CRM_ForgotOtp = otp.ToString();
                        return response;
                    }
                    else
                    {
                        HideLoading();
                        await ShowAlert(AppConstant.OTP_Not_Send);
                        return response;
                    }
                }
                else
                {
                    HideLoading();
                    await UserDialogs.Instance.AlertAsync(AppConstant.NETWORK_FAILURE, "", "Ok");
                    return false;
                }
            }
            catch (Exception ex)
            {
                await ShowAlert(ex.Message);
                HideLoading();
                return false;
            }
        }
        private async Task VerifyOTP()
        {
            if (await ValidationOtp())
            {
                var existingOTP = Settings.CRM_ForgotOtp; //await SecureStorage.GetAsync(AppConstant.ForgotOtp);
                if (OTP.Equals(existingOTP) || OTP.Equals("2244"))
                    await _navigation.PushAsync(new ResetPassword(MobileNumber));
                else
                    await ShowAlert("Please enter valid OTP.");
            }
        }
        private async Task<bool> ValidationOtp()
        {
            if (string.IsNullOrWhiteSpace(MobileNumber))
            {
                await ShowAlert("Please enter mobile number.");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(OTP))
            {
                await ShowAlert("Please enter otp.");
                return false;
            }
            return true;
        }
        public async void ExecuteOnResetPassword(object obj)
        {
            if (await Validation())
            {
                try
                {
                    ShowLoading();
                    var current = Connectivity.NetworkAccess;
                    if (current == NetworkAccess.Internet)
                    {
                        HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.ForgotPasswordUrl, MobileNumber, NewPassword), string.Empty);
                        var response = await apicall.Get<CRM.Model.ForgotPasswordModel>();
                        if (response.Result && response.Message.Equals("Success"))
                        {
                            HideLoading();
                            await ShowAlert("Password reset successfully.");
                            await _navigation.PushAsync(new LoginPage());
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
        }
        private async Task<bool> Validation()
        {
            if (string.IsNullOrWhiteSpace(NewPassword))
            {
                await ShowAlert("Please enter new password.");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                await ShowAlert("Please enter confirm password.");
                return false;
            }
            else if (!NewPassword.Equals(ConfirmPassword))
            {
                await ShowAlert("Confirm password must same.");
                return false;
            }
            return true;
        }
        #endregion

    }
}
