using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Windows.Navigation;
using System.Runtime.CompilerServices;
using Microsoft.Win32.SafeHandles;


namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}


namespace MusicPlayerGUI.settings
{
    public  record SettingsRecord(string serverUrl, int? userID, string? password);
    
    class Settings
    {
        private static string settingsUrl = @".\settings\settings.json";
        private static void SaveUpdatedData(SettingsRecord data)
        {
            string json = JsonSerializer.Serialize(data);
            File.WriteAllText(settingsUrl, json);
        }
        public static SettingsRecord GetAllSettings()
        {
            string text = File.ReadAllText(settingsUrl);
            SettingsRecord settings = JsonSerializer.Deserialize<SettingsRecord>(text);
            return settings;
        }

        public static int? GetUserID()
        {
            return GetAllSettings().userID;
        }

        public static string? GetPassword()
        {
            return GetAllSettings().password;
        }

        public static string? GetServerUrl()
        {
            return GetAllSettings().serverUrl;
        }

        public static void UpdateUserID(int? userID)
        {
            SettingsRecord settings = GetAllSettings();
            SettingsRecord UpdatedSettings = new SettingsRecord(settings.serverUrl, userID, settings.password);
            SaveUpdatedData(UpdatedSettings);
        }
        public static void UpdatePassword(string password)
        {
            SettingsRecord settings = GetAllSettings();
            SettingsRecord UpdatedSettings = new SettingsRecord(settings.serverUrl, settings.userID, password);
            SaveUpdatedData(UpdatedSettings);
        }
        public static void UpdateServerUrl(string serverUrl)
        {
            SettingsRecord settings = GetAllSettings();
            SettingsRecord UpdatedSettings = new SettingsRecord(serverUrl, settings.userID, settings.password);
            SaveUpdatedData(UpdatedSettings);
        }
    }
}
