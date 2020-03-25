using System;
using System.IO;
using System.Threading.Tasks;
using traccine.Data;
using traccine.Helpers;
using traccine.Service;
using traccine.ViewModels;
using traccine.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace traccine
{
    public partial class App : Application
    {
        static SqlDataBase database;
        private BleScannerService bleScannerService;
        public MainPageViewModel mainPageViewModel;
        public App()
        {
            try
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
                var manageview = MainPageViewModel.GetInstance;
            }catch(Exception ex)
            {

            }
        }
        public static SqlDataBase Database
        {
            get
            {
                if (database == null)
                {
                    database = new SqlDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BLD_DATA.db3"));
                }
                return database;
            }
        }

        public BleScannerService BleScannerService { get; }

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
