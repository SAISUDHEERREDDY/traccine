using Newtonsoft.Json;
using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using traccine.Helpers;
using traccine.Models;

namespace traccine.Service
{
    public class BleScannerService
    {
        private static readonly object Instancelock = new object();
        public FirbaseDataBaseHelper firebaseHelper { get; set; }
        public static BleScannerService _instance = null;
        public static BleScannerService GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Instancelock)
                    {
                        if (_instance == null)
                        {
                            _instance = new BleScannerService();
                        }
                    }

                }
                return _instance;
            }

        }

        public void Reset()
        {
            lock (BleScannerService.Instancelock)
            {
                BleScannerService._instance = null;
            }
        }
        public BleScannerService()
        {
            RunTask();
          firebaseHelper = new FirbaseDataBaseHelper();
        }
       public double getDistance(int rssi, int txPower)
        {
            /*
             * RSSI = TxPower - 10 * n * lg(d)
             * n = 2 (in free space)
             * 
             * d = 10 ^ ((TxPower - RSSI) / (10 * n))
             */

            return Math.Pow(10d, ((double)txPower - rssi) / (10 * 2));
        }
        private async void RunTask()
        {
            var ble = CrossBluetoothLE.Current;
            
            var adapter = CrossBluetoothLE.Current.Adapter;
            
            List<IDevice> deviceList = new List<IDevice>();
            adapter.DeviceDiscovered += (s, a) =>
            {

                deviceList.Add(a.Device);
            };
            while (true)
            {
                
               
                await adapter.StartScanningForDevicesAsync();
               
                foreach (var device in deviceList.ToList())
                {
                    if (device.AdvertisementRecords.Count > 0)
                    {
                        try
                        {
                            Thread.Sleep(100);
                            await adapter.ConnectToDeviceAsync(device);
                        }
                        catch (DeviceConnectionException e)
                        {
                            continue;
                        }
                        var service = await device.GetServiceAsync(Guid.Parse("ffe0ecd2-3d16-4f8d-90de-e89e7fc396a5"));
                        if (service != null)
                        {

                            string data = "";
                           
                            var characteristic3 = await service.GetCharacteristicAsync(Guid.Parse("d8de624e-140f-4a22-8594-e2216b84a5f2"));
                            var bytes3 = await characteristic3.ReadAsync();
                            if(bytes3 ==null )
                            {
                                continue;
                            }
                            data = System.Text.Encoding.UTF8.GetString(bytes3);

                            var person = await firebaseHelper.GetPersonByID(data);
                            if (data != "" ) { 
                            TimeLineModel TimeLine = new TimeLineModel();
                            TimeLine.Email = person.Email;
                            TimeLine.Name = person.Name;
                            TimeLine.Picture = person.Picture;
                            TimeLine.TransportColor = "#76c2af";
                            TimeLine.Distance = getDistance(device.Rssi,-69).ToString("0.00")+" M";
                            TimeLine.TransportType = "Walking";
                            TimeLine.DateTime = DateTime.UtcNow;
                            TimeLine.Time = DateTime.UtcNow.ToLocalTime().ToString("h:mm tt");
                             await App.Database.AddTimeLineRecord(TimeLine);
                            }
                        }
                        await adapter.DisconnectDeviceAsync(device);

                    }

                }
                deviceList.Clear();
                Thread.Sleep(10000); 

            }

        }
    }
}
