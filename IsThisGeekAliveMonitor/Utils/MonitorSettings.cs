using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsThisGeekAliveMonitor.Utils
{
    public class MonitorSettings
    {
        const string SettingsFilePath = "MonitorSettings.json";

        public MonitorSettings()
        {
            // Default values
            IsThisGeekAliveApiUrl = "http://isthisgeekalive.azurewebsites.net/api";
            GeekLoginCode = "123456";
            LoginInterval = 10;            
            NotAliveWarningWindow = 36;
            NotAliveDangerWindow = 96;            
        }

        public string IsThisGeekAliveApiUrl { get; set; }
        public string GeekUsername { get; set; }
        public string GeekLoginCode { get; set; }
        public int LoginInterval { get; set; }
        public int NotAliveWarningWindow { get; set; }
        public int NotAliveDangerWindow { get; set; }

        public static MonitorSettings Load()
        {            
            if (!File.Exists(SettingsFilePath))
            {
                return new MonitorSettings();
            }

            string fileContents = File.ReadAllText(SettingsFilePath);
            return JsonConvert.DeserializeObject<MonitorSettings>(fileContents);
        }

        public void Save()
        {
            string fileContents = JsonConvert.SerializeObject(this);
            
            if (File.Exists(SettingsFilePath))
            {
                File.Move(SettingsFilePath, SettingsFilePath + ".bak");
            }

            File.WriteAllText(SettingsFilePath, fileContents);
        }
    }
}
