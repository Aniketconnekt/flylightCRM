using Acr.UserDialogs;
using CRM.Common.Command;
using CRM.Common.Constants;
using CRM.Common.Helpers;
using CRM.Model;
using CRM.View.ForgetPassword;
using CRM.View.SignUp;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CRM.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        #region CTOR
        public LoginViewModel(INavigation navigation) : base(navigation)
        {
        }
        #endregion

        #region Commands
        public DelegateCommand LoginCommand => new DelegateCommand(ExecuteOnLogin);
        public DelegateCommand ForgetPasswordCommand => new DelegateCommand(ExecuteOnForgetPassword);
        public DelegateCommand SignUpCommand => new DelegateCommand(ExecuteOnSignUp);
        #endregion

        #region Properties
        private string _password;
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        private string _mobileNo;
        public string MobileNo
        {
            get => _mobileNo;
            set { _mobileNo = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public async void ExecuteOnLogin(object obj)
        {
            try
            {
                if (await Validation())
                {
                    ShowLoading();
                    var current = Connectivity.NetworkAccess;
                    if (current == NetworkAccess.Internet)
                    {
                        HttpClientHelper apicall = new HttpClientHelper(String.Format(ApiUrls.LoginUrl, MobileNo, Password), string.Empty);
                        var response = await apicall.Get<LoginModel>();
                        if (response != null && response.Result && response.Object != null)
                        {
                            Settings.CRM_UserId = response.Object.Id.ToString();
                            Settings.CRM_UserRole = response.Object.RoleId.ToString();
                            Settings.CRM_CreatedById = response.Object.CreatedById.ToString();
                            Settings.CRM_LoginUserName = response.Object.UserName;
                            Settings.CRM_CallNo = response.Object.CallNo;
                            Settings.CRM_WhatsappNo = response.Object.WhatsappNo;

                            //await SecureStorage.SetAsync(AppConstant.UserId, response.Object.Id.ToString());
                            //await SecureStorage.SetAsync(AppConstant.UserRole, response.Object.RoleId.ToString());
                            //await SecureStorage.SetAsync(AppConstant.CreatedById, response.Object.CreatedById.ToString());
                            //await SecureStorage.SetAsync(AppConstant.LoginUserName, response.Object.UserName);


                            App.LoginUserDetails = response.Object;
                            if (response.Object?.RoleId.ToString() == "1")
                            {
                                App.nav_bar_text_color = "#FFFFFF";
                                App.nav_bar_color = "#2699ca";
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    Application.Current.MainPage = new NavigationPage(new SuperAdmin.View.Menu.MasterDetailView())
                                    {
                                        BarBackgroundColor = Color.FromHex("#2699ca"),
                                        BarTextColor = Color.FromHex("#FFFFFF"),
                                    };
                                });
                            }
                            else if (response.Object?.RoleId.ToString() == "3")
                            {
                                App.nav_bar_text_color = "#FFFFFF";
                                App.nav_bar_color = "#2699ca";
                                App.menu_selected_text_color = "#2699ca";
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    Application.Current.MainPage = new NavigationPage(new View.Menu.MasterDetailView())
                                    {
                                        BarBackgroundColor = Color.FromHex("#2699ca"),
                                        BarTextColor = Color.FromHex("#FFFFFF"),
                                    };
                                });
                            }
                            else if (response.Object?.RoleId.ToString() == "2")
                            {
                                App.nav_bar_text_color = "#FFFFFF";
                                App.nav_bar_color = "#2baae1";
                                App.menu_selected_text_color = "#2baae1";
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    Application.Current.MainPage = new NavigationPage(new View.Menu.MasterDetailView())
                                    {
                                        BarBackgroundColor = Color.FromHex("#2baae1"),
                                        BarTextColor = Color.FromHex("#FFFFFF"),
                                    };
                                });
                            }

                            DependencyService.Get<IFileService>().CreateFile("crmTest123", "", "");
                            HideLoading();
                        }
                        else if (response != null && response.Message != "Alert")
                        {
                            HideLoading();
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await Application.Current.MainPage.DisplayAlert("Alert", response.Message, "Ok");
                            });
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
            if (string.IsNullOrWhiteSpace(MobileNo))
            {
                await ShowAlert("Please enter MobileNo.");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(Password))
            {
                await ShowAlert("Please enter password.");
                return false;
            }
            return true;
        }
        public async void ExecuteOnForgetPassword(object obj)
        {
            await _navigation.PushAsync(new ForgetPassword());
        }
        public async void ExecuteOnSignUp(object obj)
        {
            try
            {
                await _navigation.PushAsync(new Signup());
            }
            catch (Exception ex)
            {
                await ShowAlert(ex.Message);
            }
        }
        #endregion

        //public async Task<RootObject<LoginModel>> LoginAPICalling()
        //{
        //    try
        //    {
        //        //MobileNo = 6878787897;
        //        //Password = "555555";

        //        string mobileno = MobileNo.ToString();
        //        if (string.IsNullOrEmpty(mobileno) || mobileno.Length != 10)
        //        {
        //            Device.BeginInvokeOnMainThread(async () =>
        //            {
        //                await Application.Current.MainPage.DisplayAlert("Alert", "Please enter valid mobile number", "Ok");
        //            });
        //            return new RootObject<LoginModel>()
        //            {
        //                Code = 0,
        //                Msg = "Alert",
        //                AccessToken = "",
        //                Data = null
        //            };
        //        }
        //        else if (string.IsNullOrEmpty(Password))
        //        {
        //            Device.BeginInvokeOnMainThread(async () =>
        //            {
        //                await Application.Current.MainPage.DisplayAlert("Alert", "Please enter password", "Ok");
        //            });
        //            return new RootObject<LoginModel>()
        //            {
        //                Code = 0,
        //                Msg = "Alert",
        //                AccessToken = "",
        //                Data = null
        //            };
        //        }
        //        else
        //        {
        //            //var result = await restAPI.LoginAsync(MobileNo, Password);
        //            //return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new RootObject<LoginModel>()
        //        {
        //            Code = 0,
        //            Msg = ex.Message,
        //            AccessToken = "",
        //            Data = null
        //        };
        //    }
        //}
    }
}
