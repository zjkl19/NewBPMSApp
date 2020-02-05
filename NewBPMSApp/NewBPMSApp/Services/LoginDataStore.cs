using NewBPMSApp.IServices;
using NewBPMSApp.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace NewBPMSApp.Services
{
    public class LoginDataStore : ILoginDataStore
    {
        RestClient client;
        RestRequest request;


        public LoginDataStore()
        {
            client = new RestClient(App.BackendUrl);

        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public async Task<bool> LoginAsync(Login login)
        {
            if (login == null || !IsConnected)
                return false;

            //request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request = new RestRequest($"/api/Account?email={login.Email}&password={login.Password}", Method.POST);
            
            //http://218.66.5.89:8300/api/account?email=eamdfan@126.com&password=aQ!234

            //request.AddParameter("Email", login.Email);
            //request.AddParameter("Password", login.Password);
            //request.AddParameter(nameof(login.RememberMe), login.RememberMe);

            var resp = client.Execute(request);

            //var v = resp.Content;

            return resp.IsSuccessful;
        }
    }
}
