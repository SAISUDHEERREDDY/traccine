using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace traccine.Models
{
    public class TimeLineModel : INotifyPropertyChanged
    {
        public string Time { get; set; }
        public DateTime DateTime { get; set; }
        public Uri Picture { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress1 { get; set; }
        public string Adress2 { get; set; }
        public string TransportType { get; set; }
        public string TransportColor { get; set; }
        public string Distance { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
