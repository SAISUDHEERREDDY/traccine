using Newtonsoft.Json;
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
    public class ProfilePageViewModel :INotifyPropertyChanged
    {
        public UserProfile User { get; set; }
        public FirbaseDataBaseHelper firebaseHelper { get; set; }
        public ICommand UpdateCommand { get; set; }
        public int TotalActiveHours { get; set; }
        public int TotalScore { get; set; }
        public int TotalInteractions { get; set; }
        public ProfilePageViewModel()
        {
            UpdateCommand = new Command(UpdateCommandAsync);
            User = JsonConvert.DeserializeObject<UserProfile>(Settings.User);
            firebaseHelper = new FirbaseDataBaseHelper();
            TotalActiveHours = 0;
            TotalScore = 0;
            TotalInteractions = 0;
            setup();
        }

        private async void setup()
        {
            TotalActiveHours = await App.Database.GetTotalActiveHours();
            TotalInteractions = await App.Database.GetTotalInteractions();
        }

        private async void UpdateCommandAsync(object obj)
        {
             await firebaseHelper.UpdatePerson(User.Id, User.PhoneNumber);
            Settings.User = JsonConvert.SerializeObject(User);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
