using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traccine.Helpers;
using traccine.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace traccine
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell 
    {
       
        public AppShell()
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
           
            Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("HomePage", typeof(HomePage));
        }
    }
}