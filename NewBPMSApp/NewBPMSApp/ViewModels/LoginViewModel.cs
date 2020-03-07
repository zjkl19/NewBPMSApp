using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using NewBPMSApp.Models;
using NewBPMSApp.IServices;
using System.IO;
using System.Diagnostics;

namespace NewBPMSApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public ILoginDataStore DataStore => DependencyService.Get<ILoginDataStore>();
        public LoginViewModel()
        {
            Title = "登陆";

            try
            {
                if (!File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Email.txt")))
                {
                    File.Create(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Email.txt"));
                    File.Create(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Password.txt"));
                    File.Create(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RememberMe.txt"));
                }
                if (File.ReadAllText(App.RememberMeFileName).Contains("1"))
                {
                    Email = File.ReadAllText(App.EmailFileName);
                    Password = File.ReadAllText(App.PasswordFileName);
                    RememberMe = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }


            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Item;
            //    ContractChecks.Add(newItem);
            //    await DataStore.AddItemAsync(newItem);
            //});
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        string email = string.Empty;
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        string password = string.Empty;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        bool rememberMe = false;
        public bool RememberMe
        {
            get { return rememberMe; }
            set { SetProperty(ref rememberMe, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
