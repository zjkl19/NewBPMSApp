using NewBPMSApp.Models;
using NewBPMSApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewBPMSApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContractsCheckPage : ContentPage
    {
        ContractChecksViewModel viewModel;

        public ContractsCheckPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ContractChecksViewModel();

            // Subscribe to a message (which the ViewModel has also subscribed to) to display an alert
            MessagingCenter.Subscribe<ContractsCheckPage, string>(this, "确认成功！", async (sender, arg) =>
            {
                await DisplayAlert("Message received", "arg=" + arg, "OK");
            });
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Contract;
            if (item == null)
                return;

            //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ContractsListView.SelectedItem = null;
        }

        //async void AddItem_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        async void OnConfirmButton_Clicked(object sender, EventArgs e)
        {

            var thisClickedButton = sender as Button;
            var contract = (Contract)thisClickedButton.CommandParameter;

            var result=await viewModel.DataStore.UpdateItemAsync(contract);

            if(result)
            {
                await DisplayAlert("操作", "成功校核！", "OK");
            }
            else
            {
                await DisplayAlert("操作", "校核失败！", "OK");
            }
            

            viewModel.LoadItemsCommand.Execute(null);
        }
    }
}