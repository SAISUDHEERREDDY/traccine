using System;
using traccine.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace traccine
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(Settings.User))
            {
                MainPage = new MainPage(); ;

            }
            else
            {
                MainPage = new AppShell();

            }

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
