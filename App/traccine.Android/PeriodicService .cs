using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using traccine.Service;

namespace traccine.Droid
{
    [Service(Name = "com.traccine.PeriodicService", Permission = "android.permission.BIND_JOB_SERVICE", Exported = true)]
    public class PeriodicService : IntentService
    {
        private BleServer _bleServer;
        private BleScannerService bleScannerService;
        private CancellationTokenSource _cts;

        //public static  int JOB_ID = 1;

        //public static void EnqueueWork(Context context, Intent work)
        //{
        //    Java.Lang.Class cls = Java.Lang.Class.FromType(typeof(PeriodicService));
        //    try
        //    {
        //        EnqueueWork(context, cls, JOB_ID, work);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Exception: {0}", ex.Message);
        //    }
        //}


        public override IBinder OnBind(Intent intent)
        {
            return null;
        }


        //protected override void OnHandleWork(Intent p0)
        //{

        //}
        public override StartCommandResult OnStartCommand(Android.Content.Intent intent, StartCommandFlags flags, int startId)
        {
            var t = new Java.Lang.Thread(() => {
                _cts = new CancellationTokenSource();

                Task.Run(() =>
                {
                    try
                    {
                        _bleServer = new BleServer(this.ApplicationContext);
                       
                       
                    }
                    catch (Android.Accounts.OperationCanceledException)
                    {
                    }
                    finally
                    {
                        if (_cts.IsCancellationRequested)
                        {

                        }
                    }

                }, _cts.Token);
            }
            );
            t.Start();
            Task.Run(async () =>
            {
                bleScannerService = BleScannerService.GetInstance;
            //    while (true)
            //{
            //    using (var notificationManager = NotificationManager.FromContext(ApplicationContext))
            //    {
            //        var title = "Title";
            //        var channelName = "TestChannel";
            //        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            //        {
            //            NotificationChannel channel = null;
            //            if (channel == null)
            //            {
            //                channel = new NotificationChannel(channelName, channelName, NotificationImportance.Low)
            //                {
            //                    LockscreenVisibility = NotificationVisibility.Public
            //                };
            //                channel.SetShowBadge(true);
            //                notificationManager.CreateNotificationChannel(channel);
            //            }
            //            channel.Dispose();
            //        }
            //        // var bitMap = BitmapFactory.DecodeResource(Resources, Resource.Drawable.notification_template_icon_bg);
            //        var notificationBuilder = new NotificationCompat.Builder(ApplicationContext)
            //                                                                .SetContentTitle(title)
            //                                                                .SetContentText("test")
            //                                                                .SetSmallIcon(Android.Resource.Drawable.IcDialogInfo)
            //                                                                .SetShowWhen(false)
            //                                                                .SetChannelId(channelName);

            //        var notification = notificationBuilder.Build();
            //        notificationManager.Notify(0, notification);
            //    }
                Thread.Sleep(60000);
            
            });
            return StartCommandResult.Sticky;
        }

        protected override void OnHandleIntent(Intent intent)
        {
            //_bleServer = new BleServer(this.ApplicationContext);

            //Task.Run(async () =>
            //{
            //    bleScannerService = BleScannerService.GetInstance;

            //});

         //   using (var notificationManager = NotificationManager.FromContext(ApplicationContext))
         //   {
         //       var title = "Title";
         //       var channelName = "TestChannel";
         //if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
         //       {
         //           NotificationChannel channel = null;
         //           if (channel == null)
         //           {
         //               channel = new NotificationChannel(channelName, channelName, NotificationImportance.Low)
         //               {
         //                   LockscreenVisibility = NotificationVisibility.Public
         //               };
         //               channel.SetShowBadge(true);
         //               notificationManager.CreateNotificationChannel(channel);
         //           }
         //           channel.Dispose();
         //       }
         //      // var bitMap = BitmapFactory.DecodeResource(Resources, Resource.Drawable.notification_template_icon_bg);
         //       var notificationBuilder = new NotificationCompat.Builder(ApplicationContext)
         //                                                               .SetContentTitle(title)
         //                                                               .SetContentText("test")
         //                                                               .SetSmallIcon(Android.Resource.Drawable.IcDialogInfo)
         //                                                               .SetShowWhen(false)
         //                                                               .SetChannelId(channelName);

         //       var notification = notificationBuilder.Build();
         //       notificationManager.Notify(0, notification);
         //   }

        }
    }
}