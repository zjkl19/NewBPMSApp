using NewBPMSApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
            string Email = viewModel.Email;
            string Password = viewModel.Password;
            bool RememberMe = viewModel.RememberMe;

            var login = new Models.Login
            {
                Email=Email,
                Password=Password,
                RememberMe= RememberMe
            };

            var result = await viewModel.DataStore.LoginAsync(login);

            if (RememberMe)
            {
                //账号、密码

                File.WriteAllText(App.EmailFileName, EmailEntry.Text);
                File.WriteAllText(App.PasswordFileName, PasswordEntry.Text);
                File.WriteAllText(App.RememberMeFileName, "1");
    
            }
            else
            {
                File.WriteAllText(App.EmailFileName, "");
                File.WriteAllText(App.PasswordFileName, "");
                File.WriteAllText(App.RememberMeFileName, "0");
            }

            if (result)
            {
                //await Navigation.PushModalAsync(new ContentPage(new AppShell()));
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await DisplayAlert("服务器返回消息", "登陆失败，请检查Email或密码", "确认");
            }
        }
    }
}