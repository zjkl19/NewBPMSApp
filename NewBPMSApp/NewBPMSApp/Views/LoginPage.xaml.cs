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
    public partial class LoginPage : ContentPage
    {
        LoginViewModel viewModel;
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new LoginViewModel();
        }

        async void OnLoginButton_Clicked(object sender, EventArgs e)
        {
            string Email = EmailEntry.Text;
            string Password = PasswordEntry.Text;

            var login = new Models.Login
            {
                Email=Email,
                Password=Password,
                RememberMe=false
            };

            var result = await viewModel.DataStore.LoginAsync(login);

            if (result)
            {
                await Navigation.PushModalAsync(new NavigationPage(new AppShell()));
            }
            else
            {
                await DisplayAlert("服务器返回消息", "登陆失败，请检查Email或密码", "确认");
            }
        }
    }
}