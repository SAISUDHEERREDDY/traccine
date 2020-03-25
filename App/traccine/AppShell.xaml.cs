using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using traccine.Helpers;
using traccine.Models;
using traccine.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace traccine
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell 
    {
        public ICommand HelpCommand => new Command(() => this.DisplayAlert("Logout", null, "ok"));
        public AppShell()
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
           
            Routing.RegisterRoute("LoginPage", typeof(MainPage));
            Routing.RegisterRoute("ProfilePage", typeof(ProfilePage));
            Routing.RegisterRoute("HomePage", typeof(HomePage));
            Routing.RegisterRoute("TimelinePage", typeof(Timeline));
        }
    }
}