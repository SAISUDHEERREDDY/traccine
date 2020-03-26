using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using traccine.Helpers;
using traccine.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace traccine.ViewModels
{
    public class ProfilePageViewModel :INotifyPropertyChanged
    {
        public UserProfile User { get; set; }
        public FirbaseDataBaseHelper firebaseHelper { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand ShareCommand { get; set; }
        public int TotalActiveHours { get; set; }
        public int TotalScore { get; set; }
        public int TotalInteractions { get; set; }
        public FcmMessageHelper messageHelper;
        public ProfilePageViewModel()
        {
            UpdateCommand = new Command(UpdateCommandAsync);
            ShareCommand = new Command(SahreCommand);
            User = JsonConvert.DeserializeObject<UserProfile>(Settings.User);
            firebaseHelper = new FirbaseDataBaseHelper();
            messageHelper = new FcmMessageHelper();
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
            await firebaseHelper.Updateisinfected(User.Id, User.IsInfected);
            if (User.IsInfected)
            {
                var users = await App.Database.GetTotalInteractedEmails();

                foreach (var user in users)
                {
                    var _interactedUser = await  firebaseHelper.GetPerson(user);
                    if(_interactedUser.FcmToken != "")
                    {
                       await  messageHelper.NotifyAsync(_interactedUser.FcmToken, "AmiSafe detected a person you meet is infected with Corona", User.Name + " is infected with Corona ");
                    }
                }
            }
        }
        public async void SahreCommand(object obj)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = GlobalSettings.AppUrl,
                Title = "Share AmiSafe",
                Text= "Hey, I'm using AmiSafe App, To break the Corona Virus Chain. Please install this app and join with me. "

            });
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
