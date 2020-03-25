using Newtonsoft.Json;
using Plugin.GoogleClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traccine.Helpers;
using traccine.Models;
using traccine.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace traccine.Renderers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutHeader : ContentView
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IGoogleClientManager _googleClientManager;
        public FlyoutHeader()
        {
            InitializeComponent();
            _googleClientManager = CrossGoogleClient.Current;
            BindingContext = new FlyoutHeaderViewModel();
        }
        public async void LogoutCommand(object sender, EventArgs e)
        {
            _googleClientManager.Logout();
            Settings.User = "";
        }
    }
}