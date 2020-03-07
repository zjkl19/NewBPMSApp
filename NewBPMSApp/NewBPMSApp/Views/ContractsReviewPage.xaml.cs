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
    public partial class ContractsReviewPage : ContentPage
    {
        ContractReviewsViewModel viewModel;

        public ContractsReviewPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ContractReviewsViewModel();

            // Subscribe to a message (which te ViewModel has also subscribed to) to display an alert
            MessagingCenter.Subscribe<ContractsReviewPage, string>(this, "确认成功！", async (sender, arg) =>
            {
                await DisplayAlert("Message received", "arg=" + arg, "OK");
            });
        }

        //async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        //{
        //    var item = args.SelectedItem as DetailsContract;
        //    if (item == null)
        //        return;

        //    //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

        //    // Manually deselect item.
        //    ContractsListView.SelectedItem = null;
        //}

        async void OnItemTapped(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as DetailsContract;
            if (item == null)
                return;

            await Navigation.PushAsync(new UserProductValuePage(new UserProductValueViewModel (item.UserProductValueViewModels)));

            // Manually deselect item.
            //ContractsListView.SelectedItem = null;
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

            contract.ReviewStatus = ReviewStatus.Reviewed;

            var result = await viewModel.DataStore.UpdateItemAsync(contract);

            if (result)
            {
                await DisplayAlert("服务器返回消息", "成功审核！", "确认");
            }
            else
            {
                await DisplayAlert("服务器返回消息", "审核失败！", "确认");
            }

            viewModel.LoadItemsCommand.Execute(null);
        }

        async void OnRestoreButton_Clicked(object sender, EventArgs e)
        {
            var thisClickedButton = sender as Button;
            var contract = (Contract)thisClickedButton.CommandParameter;

            contract.SubmitStatus = (int)SubmitStatus.NotSubmitted;
            contract.CheckStatus = CheckStatus.NotChecked;
            contract.ReviewStatus = (int)ReviewStatus.NotReviewed;
            contract.FinishStatus = (int)FinishStatus.NotFinished;

            var result = await viewModel.DataStore.UpdateItemAsync(contract);

            if (result)
            {
                await DisplayAlert("服务器返回消息", "成功回退！", "确认");
            }
            else
            {
                await DisplayAlert("服务器返回消息", "回退失败！", "确认");
            }

            viewModel.LoadItemsCommand.Execute(null);
        }
    }
}