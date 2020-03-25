using Newtonsoft.Json;
using Plugin.GoogleClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using traccine.Helpers;
using traccine.Models;
using Xamarin.Forms;

namespace traccine.ViewModels
{
    public class FlyoutHeaderViewModel : INotifyPropertyChanged
    {
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public Uri _Picture { get; set; }

        public Uri Picture
        {
            get { return _Picture; }
            set
            {
                if (_Picture != value)
                {
                    _Picture = value;
                    OnPropertyChanged("Picture");
                }
            }
        }
        public string _Email { get; set; }

        public string Email
        {
            get { return _Email; }
            set
            {
                if (_Email != value)
                {
                    _Email = value;
                    OnPropertyChanged("Email");
                }
            }
        }
        public MainPageViewModel mainPageViewModel;
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand LogoutCommand { get; set; }
        public FlyoutHeaderViewModel()
        {
            LogoutCommand = new Command(Logout);
            //mainPageViewModel = new MainPageViewModel();
              var user = JsonConvert.DeserializeObject<UserProfile>(Settings.User);
            if (user != null)
            {
                Picture = user.Picture;
                Email = user.Email;

            }
        }
        public void Logout()
        {
            MainPageViewModel.GetInstance.Logout();
        }
    }
}
