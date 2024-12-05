using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunDraw.Types;
using System.Net.Http.Headers;
using System.Net;

namespace FunDraw
{
    public class Session
    {
        public Session()
        {
            accessToken = LocalStorage.GetAccessToken();
            refreshToken = LocalStorage.GetRefreshToken();
        }
        public static string accessToken { get; set; } = "";
        public static string refreshToken { get; set; } = "";

        public static async Task Login(string username, string password)
        {
            //var userCredentials = new Dictionary<string, string>
            //{
            //    { "username", username },
            //    { "password", password }
            //};

            //JObject response = await HTTPClient.PostFormUrlEncodedAsync($"{AppConfig.APP_API_HOST}/auth/login", userCredentials);
            //if (response.ContainsKey("Error")) return;
            //var data = JsonConvert.DeserializeObject<Types.Login>(response.ToString());
            //LocalStorage.SetAccessToken(data.data.accessToken);
            //LocalStorage.SetRefreshToken(data.data.refreshToken);

            string apiUrl = $"{AppConfig.APP_API_HOST}/auth/login";
            var credentials = new { Username = username, Password = password };

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(credentials);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    string jwt = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Types.Login>(jwt);
                    LocalStorage.SetAccessToken(data.data.accessToken);
                    LocalStorage.SetRefreshToken(data.data.refreshToken);
                    MessageBox.Show("Login successful \n" + "JWT: " + jwt);
                }
                else
                {
                    MessageBox.Show("Authentication failed: " + response.StatusCode);
                }
            }
        }

        public static async Task Register(string username, string password, string email)
        {
            string apiUrl = $"{AppConfig.APP_API_HOST}/auth/register";
            var credentials = new { Username = username, Password = password, Email = email };
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(credentials);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    string jwt = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Types.Login>(jwt);
                    LocalStorage.SetAccessToken(data.data.accessToken);
                    LocalStorage.SetRefreshToken(data.data.refreshToken);
                    MessageBox.Show("Register successful \n" + "JWT: " + jwt);
                }
                else
                {
                    MessageBox.Show("Authentication failed: " + response.StatusCode);
                }
            }
        }

        public static async Task<string> RefreshToken()
        {
            //string refreshToken = LocalStorage.GetRefreshToken();
            //JObject response = await HTTPClient.PostAsync($"{AppConfig.APP_API_HOST}/auth/refresh-token", $"refreshToken={refreshToken}");
            //if (response.ContainsKey("Error")) return;
            //var data = JsonConvert.DeserializeObject<Types.Login>(response.ToString());
            //LocalStorage.SetAccessToken(data.data.accessToken);
            //LocalStorage.SetRefreshToken(data.data.refreshToken);

            string apiUrl = $"{AppConfig.APP_API_HOST}/auth/refresh-token";
            string refreshToken = LocalStorage.GetRefreshToken();
            var refreshData = new { RefreshToken = refreshToken };
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(refreshData);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine("Token refresh failed: " + response.StatusCode);
                    return null;
                }
            }
        }

        public static async Task<JObject> GET(string path, string? queryParams = "")
        {
            string accessToken = LocalStorage.GetAccessToken();
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {accessToken}" }
            };
            JObject response = await HTTPClient.GetAsync($"{AppConfig.APP_API_HOST}/{path}", queryParams, headers);
            return response;
        }


    }
}
