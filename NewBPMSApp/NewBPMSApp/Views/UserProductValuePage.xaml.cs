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
    public partial class UserProductValuePage : ContentPage
    {
        UserProductValueViewModel viewModel;
        public UserProductValuePage(UserProductValueViewModel model)
        {
            InitializeComponent();
            BindingContext = viewModel = model;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}