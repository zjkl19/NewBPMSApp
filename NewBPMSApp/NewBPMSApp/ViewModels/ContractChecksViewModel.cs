using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using NewBPMSApp.Models;
using NewBPMSApp.Views;

namespace NewBPMSApp.ViewModels
{
    public class ContractChecksViewModel : CommonDataBaseViewModel<Contract>
    {
        public ObservableCollection<Contract> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ContractChecksViewModel()
        {
            Title = "合同校核";
            Items = new ObservableCollection<Contract>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Item;
            //    ContractChecks.Add(newItem);
            //    await DataStore.AddItemAsync(newItem);
            //});
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}