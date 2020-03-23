using Newtonsoft.Json;
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
       
        public FlyoutHeader()
        {
            InitializeComponent();
            BindingContext = new FlyoutHeaderViewModel();
        }
    }
}