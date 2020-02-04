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

        string EmailFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Email.txt");
        string PasswordFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Password.txt");
        string RememberMeFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RememberMe.txt");


        public LoginPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new LoginViewModel();

            if(!File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Email.txt")))
            {
                File.Create(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Email.txt"));
                File.Create(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Password.txt"));
                File.Create(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RememberMe.txt"));
            }
            if (File.ReadAllText(RememberMeFileName).Contains("1"))
            {
                EmailEntry.Text = File.ReadAllText(EmailFileName);
                PasswordEntry.Text = File.ReadAllText(PasswordFileName);
                RememberMeCheckBox.IsChecked = true;
            }

        }

        async void OnLoginButton_Clicked(object sender, EventArgs e)
        {
            string Email = EmailEntry.Text;
            string Password = PasswordEntry.Text;
            bool RememberMe = RememberMeCheckBox.IsChecked;

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

                File.WriteAllText(EmailFileName, EmailEntry.Text);
                File.WriteAllText(PasswordFileName, PasswordEntry.Text);
                File.WriteAllText(RememberMeFileName, "1");
                //cookie Name,value
                //editor.PutString("cookieName", cookieName);
                //editor.PutString("cookieValue", cookieValue);
                //remember me
                //editor.PutBoolean("rememberPass", true);// editor.Commit();     
            }
            else
            {
                File.WriteAllText(EmailFileName, "");
                File.WriteAllText(PasswordFileName, "");
                File.WriteAllText(RememberMeFileName, "0");
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