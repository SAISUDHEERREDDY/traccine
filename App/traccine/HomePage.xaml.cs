using traccine.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace traccine
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new HomePageViewModel();
        }
    }
}
