using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NewBPMSApp.Services;
using NewBPMSApp.Views;
using System.IO;

namespace NewBPMSApp
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static string BackendUrl ="http://218.66.5.89:8300";
        public static string AzureBackendUrl =
            DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";

        public static bool UseMockDataStore = false;

        public static string EmailFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Email.txt");
        public static string PasswordFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Password.txt");
        public static string RememberMeFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RememberMe.txt");
        public static string CookieName { get; set; }
        public static string CookieValue { get; set; }


        public App()
        {
            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<ContractCheckDataStore>();
            //DependencyService.Register<AzureDataStore>();

            DependencyService.Register<LoginDataStore>();
            DependencyService.Register<ContractReviewDataStore>();

            MainPage = new LoginPage();

            //MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
