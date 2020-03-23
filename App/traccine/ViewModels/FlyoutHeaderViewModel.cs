using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using traccine.Helpers;
using traccine.Models;

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
        public event PropertyChangedEventHandler PropertyChanged;

        public FlyoutHeaderViewModel()
        {
            var user = JsonConvert.DeserializeObject<UserProfile>(Settings.User);
            if (user != null)
            {
                Picture = user.Picture;
                Email = user.Email;

            }
        }
    }
}
