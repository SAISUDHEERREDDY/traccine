using Newtonsoft.Json;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using traccine.Helpers;
using traccine.Models;

namespace traccine.ViewModels
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public string _Message { get; set; }
        public int Interactions { get; set; }
        public int ActiveHours { get; set; }
        public int Places { get; set; }
        public int Score { get; set; }
        public String Username { get; set; }
        public string Message
        {
            get { return _Message; }
            set
            {
                if (_Message != value)
                {
                    _Message = value;
                    OnPropertyChanged("Message");
                }
            }
        }
        public HomePageViewModel()
        {
            //setupclient();
            Message = "";
            Places = 0;
            Interactions = 0;
            Score = 0;
            ActiveHours = 0;
            Username = "";
            Setup();
        }

        private async void Setup()
        {
            Username = JsonConvert.DeserializeObject<UserProfile>(Settings.User).Name;
            ActiveHours= await App.Database.GetActiveHours();
            Interactions = await App.Database.GetInteraction();
            if (Interactions == 0)
            {
                Score = 100;
            }
            else
            {
                Score = 0;
            }
            if (DateTime.Now.Hour <= 12)
            {
                Message= "Good Morning";
            }
            else if (DateTime.Now.Hour <= 16)
            {
                Message = "Good Afternoon";
            }
            else if (DateTime.Now.Hour <= 20)
            {
                Message = "Good Evening";
           }
            else
            {
                Message = "Good Night";
           }
        }

        public async void setupclient()
        {
            var ble = CrossBluetoothLE.Current;
            var adapter = CrossBluetoothLE.Current.Adapter;
           
            List<IDevice> deviceList = new List<IDevice> ();
            adapter.DeviceDiscovered += (s, a) => deviceList.Add(a.Device);
            await adapter.StartScanningForDevicesAsync();
            foreach(var device in deviceList)
            {
                if(device.AdvertisementRecords.Count > 3 )
                {
                    var data = device.AdvertisementRecords[2].ToString();
                    try
                    {
                        await adapter.ConnectToDeviceAsync(device);
                    }
                    catch (DeviceConnectionException e)
                    {
                        continue;
                    }
                    var service = await device.GetServiceAsync(Guid.Parse("ffe0ecd2-3d16-4f8d-90de-e89e7fc396a5"));
                    if (service != null)
                    {
                        var characteristic = await service.GetCharacteristicAsync(Guid.Parse("d8de624e-140f-4a22-8594-e2216b84a5f2"));
                        var bytes = await characteristic.ReadAsync();
                        string result = System.Text.Encoding.UTF8.GetString(bytes);
                        Message = result;
                    }
                   

                }

            }
           
        }
      
    }
}
