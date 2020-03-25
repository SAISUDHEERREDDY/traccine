using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace traccine.Models
{
    public class UserProfile : INotifyPropertyChanged
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Uri Picture { get; set; }
        public string PhoneNumber { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
