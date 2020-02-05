﻿using NewBPMSApp.IServices;
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
        HttpClient client;
        ContractCheck viewModels;
        IEnumerable<Contract> items;

        RestClient client1;
        RestRequest request;

        public ContractCheckDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.BackendUrl}/");

            viewModels = new ContractCheck();

            items = new List<Contract>();
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<IEnumerable<Contract>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/Contract");
                viewModels = await Task.Run(() => JsonConvert.DeserializeObject<ContractCheck>(json));
                items = viewModels.ContractViewModels;
            }

            return items;
        }

        public async Task<Contract> GetItemAsync(Guid id)
        {
            if (id != null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/item/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Contract>(json));
            }

            return null;
        }

        public async Task<bool> UpdateItemAsync(Contract item)
        {
            if (item == null || item.Id == null || !IsConnected)
                return false;

            IRestResponse resp = null;

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            //var response = await client.PutAsync(new Uri($"api/Contract/{item.Id}"), byteContent);

            client1 = new RestClient(App.BackendUrl);
            request = new RestRequest($"/api/Contract/{item.Id}", Method.PUT);
            request.AddJsonBody(item);

            request.AddCookie(App.CookieName, App.CookieValue);

            //request.AddHeader("Content-type", "application/json");
            //request.AddParameter("Id", item.Id);

            try
            {
                resp = await client1.ExecuteAsync(request);

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
