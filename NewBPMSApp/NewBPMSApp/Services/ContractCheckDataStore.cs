using NewBPMSApp.IServices;
using NewBPMSApp.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace NewBPMSApp.Services
{
    public class ContractCheckDataStore : ICommonDataStore<Contract>
    {
        //HttpClient client;
        ContractCheck viewModels;
        IEnumerable<Contract> items;
        readonly RestClient client;
        RestRequest request;

        public ContractCheckDataStore()
        {
            //Obseleted code(using HttpClient)
            //client = new HttpClient();
            //client.BaseAddress = new Uri($"{App.BackendUrl}/");
            client = new RestClient(App.BackendUrl);
            
            viewModels = new ContractCheck();

            items = new List<Contract>();
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<IEnumerable<Contract>> GetItemsAsync(bool forceRefresh = false)
        {
            IRestResponse resp = null;
            if (forceRefresh && IsConnected)
            {
                request = new RestRequest($"/api/CheckContract/", Method.GET);

                request.AddCookie(App.CookieName, App.CookieValue);
                try
                {
                    resp = await client.ExecuteAsync(request);

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

                //Obseleted code(using HttpClient)
                //var json = await client.GetStringAsync($"api/CheckContract");
                var json = resp.Content;
                viewModels = await Task.Run(() => JsonConvert.DeserializeObject<ContractCheck>(json));
                items = viewModels.ContractViewModels;
            }

            return items;
        }

        public async Task<Contract> GetItemAsync(Guid id)
        {
            IRestResponse resp = null;
            if (id != null && IsConnected)
            {
                request = new RestRequest($"/api/CheckContract/", Method.GET);

                request.AddCookie(App.CookieName, App.CookieValue);
                try
                {
                    resp = await client.ExecuteAsync(request);

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                var json = resp.Content;

                //Obseleted code(using HttpClient)
                //var json = await client.GetStringAsync($"api/item/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Contract>(json));
            }

            return null;
        }

        public async Task<bool> UpdateItemAsync(Contract item)
        {
            if (item == null || item.Id == null || !IsConnected)
                return false;

            IRestResponse resp = null;

            //Obseleted code(using HttpClient)
            //var serializedItem = JsonConvert.SerializeObject(item);
            //var buffer = Encoding.UTF8.GetBytes(serializedItem);
            //var byteContent = new ByteArrayContent(buffer);
            //var response = await client.PutAsync(new Uri($"api/Contract/{item.Id}"), byteContent);
            //client1 = new RestClient(App.BackendUrl);

            request = new RestRequest($"/api/CheckContract/{item.Id}", Method.PUT);
            request.AddJsonBody(item);

            request.AddCookie(App.CookieName, App.CookieValue);

            //request.AddHeader("Content-type", "application/json");
            //request.AddParameter("Id", item.Id);

            try
            {
                resp = await client.ExecuteAsync(request);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            //var v = resp.Content;
            if (resp == null)
            {
                return false;
            }

            return resp.IsSuccessful;
            //return response.IsSuccessStatusCode;
        }
    }
}
