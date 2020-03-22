using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChasedHomeUIDesign
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            CheckUserPermissions();
        }
        private async void CheckUserPermissions()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need location", "App needs location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Location });
                    status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted)
                {

                }
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception ex)
            {

            }
        }

        void LoginClick(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }
    }
}
