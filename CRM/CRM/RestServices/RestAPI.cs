using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using CRM.Model;
using CRM.ViewModel;

namespace CRM.RestServices
{
    class RestAPI
    {
        //UserAPI
        public string Baseurl = "http://flylight.connekt.in//api/Service/";
        public string GetCampaignUrl = "http://flylight.connekt.in//api/Service/GetCampaignList";
        public string GetLadAction = "http://flylight.connekt.in//api/Service/GetLadActionList";
        public string ForgotPasswordUrl = "http://flylight.connekt.in//api/Service/";
        public string CheckMobileNoUrl = "http://flylight.connekt.in//api/Service/";
        public string GetLeadUrl = "http://flylight.connekt.in/api/Service/";
        public string GetLeadCountOnDashBoardAPI = "http://flylight.connekt.in//api/Service/";

        HttpClient client;

        public async Task<RootObject<DashboardLADCountModel>> GetLeadCountOnDashboardAsync(long UserId)
        {
            try
            {
                client = new HttpClient();
                var response = client.GetAsync(GetLeadCountOnDashBoardAPI + "GetDashboardData?Userid=" + UserId).ConfigureAwait(false).GetAwaiter().GetResult();
                var Data = response.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<DashboardLADCountModel>(Data);

                return new RootObject<DashboardLADCountModel>()
                {
                    Code = 0,
                    Msg = "Sucess",
                    AccessToken = "",
                    Data = model
                };
                //return null;
            }
            catch (Exception ex)
            {
                string x = ex.Message;
                return new RootObject<DashboardLADCountModel>()
                {
                    Code = 0,
                    Msg = x,
                    AccessToken = "",
                    Data = null
                };
            }
        }
        public async Task<RootObject<NewLeadModel>> GetLeadAsync(long UserId,long LastRecordId)
        {
            try
            {
                client = new HttpClient();
                var response = client.GetAsync(GetLeadUrl + "GetLeads?userid=" + UserId + "&&lastrecordsid=" + LastRecordId).ConfigureAwait(false).GetAwaiter().GetResult();
                var Data = response.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<NewLeadModel>(Data);

                return new RootObject<NewLeadModel>()
                {
                    Code = 0,
                    Msg = "Sucess",
                    AccessToken = "",
                    Data = model
                };
                //return null;
            }
            catch (Exception ex)
            {
                string x = ex.Message;
                return new RootObject<NewLeadModel>()
                {
                    Code = 0,
                    Msg = x,
                    AccessToken = "",
                    Data = null
                };
            }
        }
        public async Task<RootObject<bool>> CheckMobileNoAsync(long MobileNo)
        {
            try
            {
                client = new HttpClient();
                var response = client.GetAsync(CheckMobileNoUrl + "CheckMobileNoExist?mobileno=" + MobileNo ).ConfigureAwait(false).GetAwaiter().GetResult();
                var Data = response.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<bool>(Data);

                return new RootObject<bool>()
                {
                    Code = 0,
                    Msg = "Sucess",
                    AccessToken = "",
                    Data = model
                };
                //return null;
            }
            catch (Exception ex)
            {
                string x = ex.Message;
                return new RootObject<bool>()
                {
                    Code = 0,
                    Msg = x,
                    AccessToken = "",
                    Data = false
                };
            }
        }
        public async Task<RootObject<ForgotPasswordModel>> ForgotPasswordAsync(long MobileNo, string Password)
        {
            try
            {
                client = new HttpClient();
                var response = client.GetAsync(ForgotPasswordUrl + "ForGotPassword?MobileNumber=" + MobileNo + "&&NewPassword=" + Password).ConfigureAwait(false).GetAwaiter().GetResult();
                var Data = response.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<ForgotPasswordModel>(Data);

                return new RootObject<ForgotPasswordModel>()
                {
                    Code = 0,
                    Msg = "Sucess",
                    AccessToken = "",
                    Data = model
                };
                //return null;
            }
            catch (Exception ex)
            {
                string x = ex.Message;
                return new RootObject<ForgotPasswordModel>()
                {
                    Code = 0,
                    Msg = x,
                    AccessToken = "",
                    Data = null
                };
            }
        }
        public async Task<RootObject<LoginModel>> LoginAsync(long MobileNo,string Password)
        {
            try
            {
                client = new HttpClient();
                var response = client.GetAsync(Baseurl + "Login?MobileNumber=" + MobileNo + "&&Password=" + Password).ConfigureAwait(false).GetAwaiter().GetResult(); 
                var Data = response.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<LoginModel>(Data);

                return new RootObject<LoginModel>()
                {
                    Code=0,
                    Msg="Sucess",
                    AccessToken="",
                    Data=model
                };
                //return null;
            }
            catch (Exception ex)
            {
                string x = ex.Message;
                return new RootObject<LoginModel>()
                {
                    Code = 0,
                    Msg = x,
                    AccessToken = "",
                    Data = null
                }; 
            }
        }
        public async Task<RootObject<NewCampaignModel>> GetCampignList()
        {
            try
            {
                client = new HttpClient();
                var response = client.GetAsync(GetCampaignUrl).ConfigureAwait(false).GetAwaiter().GetResult(); 
                var Data = response.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<NewCampaignModel>(Data);
                return new RootObject<NewCampaignModel>()
                {
                    Code = 0,
                    Msg = "Sucess",
                    AccessToken = "",
                    Data = model
                };
            }
            catch (Exception ex)
            {
                string x = ex.Message;
                return new RootObject<NewCampaignModel>()
                {
                    Code = 0,
                    Msg = x,
                    AccessToken = "",
                    Data = null
                };
            }
        }
        public async Task<RootObject<LadActionModel>> GetLadActions()
        {
            try
            {
                client = new HttpClient();
                var response = client.GetAsync(GetLadAction).ConfigureAwait(false).GetAwaiter().GetResult(); ;
                var Data = response.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<LadActionModel>(Data);
                return new RootObject<LadActionModel>()
                {
                    Code = 0,
                    Msg = "Sucess",
                    AccessToken = "",
                    Data = model
                };
            }
            catch (Exception ex)
            {
                string x = ex.Message;
                return new RootObject<LadActionModel>()
                {
                    Code = 0,
                    Msg = x,
                    AccessToken = "",
                    Data = null
                };
            }
        }
    }
}
