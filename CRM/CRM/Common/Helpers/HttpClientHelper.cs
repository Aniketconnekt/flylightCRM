using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Common.Helpers
{
    public class HttpClientHelper
    {
        protected readonly string _endpoint;
        protected readonly string _accesstoken;

        public HttpClientHelper(string endpoint, string accesstoken)
        {
            _endpoint = endpoint;
            _accesstoken = accesstoken;
        }

        // This Method used for POST APIs 
        public async Task<T> Post<T>(string jsonobject)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(60);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (!string.IsNullOrWhiteSpace(_accesstoken))
                    httpClient.DefaultRequestHeaders.Add("token", _accesstoken);

                var response = await httpClient.PostAsync(_endpoint, new StringContent(jsonobject, Encoding.UTF8, "application/json")).ConfigureAwait(false);
                return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
            }
        }

        // This Method used for Get APIs
        public async Task<T> Get<T>()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(60);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (!string.IsNullOrWhiteSpace(_accesstoken))
                    httpClient.DefaultRequestHeaders.Add("token", _accesstoken);

                var response = await httpClient.GetAsync(_endpoint, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
            }
        }

        public async Task<bool> Post<T>(byte[] fileDataBytes, string fileName)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var endpoint = _endpoint;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", _accesstoken);
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/vnd.ms-excel");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                    using (var formData = new MultipartFormDataContent())
                    {
                        formData.Add(new ByteArrayContent(fileDataBytes), "ClientDocs", fileName);
                        var response = await client.PostAsync(endpoint, formData);
                        if (response.StatusCode == HttpStatusCode.OK) 
                            return true;
                        else
                            return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public async Task<string> PostAsync<T>(byte[] fileDataBytes, string fileName)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var endpoint = _endpoint;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", _accesstoken);
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/vnd.ms-excel");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                    using (var formData = new MultipartFormDataContent())
                    {
                        formData.Add(new ByteArrayContent(fileDataBytes), "ClientDocs", fileName);
                        var response = await client.PostAsync(endpoint, formData);
                        if (response.StatusCode == HttpStatusCode.OK)
                            return "Success";
                        else
                            return "UnSuccessfull";
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }


    }
}
