using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traccine.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace traccine.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage ,INotifyPropertyChanged
    {
       
        public MainPage()
        {
           
            CheckUserPermissions();
            BindingContext = MainPageViewModel.GetInstance;
            Shell.SetTabBarIsVisible(this, false);
            Shell.SetNavBarIsVisible(this, false);
            Shell.SetFlyoutBehavior(this, 0);
            NavigationPage.SetHasBackButton(this, false);
            SetValue(NavigationPage.HasNavigationBarProperty, false);
          
            InitializeComponent();
           
          
        }
        public async void CheckUserPermissions()
        {
            try
            {
                var locationstatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (locationstatus != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need location", "App needs location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Location });
                    locationstatus = results[Permission.Location];
                }

                if (locationstatus == PermissionStatus.Granted)
                {

                }
                else if (locationstatus != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                }

                var Storagestatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (Storagestatus != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
                    {
                        await DisplayAlert("Need Storage", "App needs Storage", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Storage });
                    Storagestatus = results[Permission.Storage];
                }

                if (Storagestatus == PermissionStatus.Granted)
                {

                }
                else if (Storagestatus != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Storage Denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}