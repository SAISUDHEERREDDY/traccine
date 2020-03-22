using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using Shiny.BluetoothLE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Text;

namespace ChasedHomeUIDesign.ViewModels
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
            setupclient();
            Message = "";
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
                if(device.AdvertisementRecords.Count > 3 && device.Name== "traccine")
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
                    var characteristic = await service.GetCharacteristicAsync(Guid.Parse("d8de624e-140f-4a22-8594-e2216b84a5f2"));
                    var bytes = await characteristic.ReadAsync();
                    string result = System.Text.Encoding.UTF8.GetString(bytes);
                    Message = result;

                }

            }
           
        }
      
    }
}
