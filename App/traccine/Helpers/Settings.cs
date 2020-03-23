using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace traccine.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }
        public static string User
        {
            get
            {
                return AppSettings.GetValueOrDefault("user", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("user", value.ToString());
            }
        }
    }
}
