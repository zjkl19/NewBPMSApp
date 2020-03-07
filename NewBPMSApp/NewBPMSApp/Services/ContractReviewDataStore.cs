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
    public class ContractReviewDataStore : IContractReviewDataStore<DetailsContract,Contract>
    {
        //Obseleted code(using HttpClient)
        //HttpClient client;
        ContractReview viewModels;
        IEnumerable<DetailsContract> items;

        RestClient client;
        RestRequest request;

        public ContractReviewDataStore()
        {
            //Obseleted code(using HttpClient)
            //client = new HttpClient();
            //client.BaseAddress = new Uri($"{App.BackendUrl}/");

            client = new RestClient(App.BackendUrl);
            viewModels = new ContractReview();

            items = new List<DetailsContract>();
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<IEnumerable<DetailsContract>> GetItemsAsync(bool forceRefresh = false)
        {
            IRestResponse resp = null;
            if (forceRefresh && IsConnected)
            {
                request = new RestRequest($"/api/ReviewContract/", Method.GET);

                request.AddCookie(App.CookieName, App.CookieValue);
                try
                {
                    resp = await client.ExecuteAsync(request);

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

                //var json = await client.GetStringAsync($"api/ReviewContract");
                var json = resp.Content;
                viewModels = await Task.Run(() => JsonConvert.DeserializeObject<ContractReview>(json));
                items = viewModels.DetailsContractViewModels;
            }

            return items;
        }

        
        //public async Task<Contract> GetItemAsync(Guid id)
        //{
        //    if (id != null && IsConnected)
        //    {
        //        var json = await client.GetStringAsync($"api/item/{id}");
        //        return await Task.Run(() => JsonConvert.DeserializeObject<Contract>(json));
        //    }

        //    return null;
        //}

        public async Task<bool> UpdateItemAsync(Contract item)
        {
            if (item == null || item.Id == null || !IsConnected)
                return false;

            IRestResponse resp = null;

            //var serializedItem = JsonConvert.SerializeObject(item);
            //var buffer = Encoding.UTF8.GetBytes(serializedItem);
            //var byteContent = new ByteArrayContent(buffer);

            //var response = await client.PutAsync(new Uri($"api/Contract/{item.Id}"), byteContent);

            client = new RestClient(App.BackendUrl);
            request = new RestRequest($"/api/ReviewContract/{item.Id}", Method.PUT);
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
