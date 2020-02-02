using NewBPMSApp.IServices;
using NewBPMSApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    }
}
