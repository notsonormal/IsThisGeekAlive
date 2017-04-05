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
        const string SettingsFileName = "MonitorSettings.json";

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
            string settingsFilePath = Path.Combine(ProjectUtils.GetSettingsDirectory(), SettingsFileName);
              
            if (!File.Exists(settingsFilePath))
            {
                return new MonitorSettings();
            }

            string fileContents = File.ReadAllText(settingsFilePath);
            return JsonConvert.DeserializeObject<MonitorSettings>(fileContents);
        }

        public void Save()
        {
            string fileContents = JsonConvert.SerializeObject(this);

            string settingsDirectory = ProjectUtils.GetSettingsDirectory();
            string settingsFilePath = Path.Combine(settingsDirectory, SettingsFileName);
            string settingsFileBackupPath = settingsFilePath + ".bak";

            if (!Directory.Exists(settingsDirectory))
                Directory.CreateDirectory(settingsDirectory);

            if (File.Exists(settingsFilePath))
            {
                if (File.Exists(settingsFileBackupPath))
                {
                    File.Delete(settingsFileBackupPath);
                }

                File.Move(settingsFilePath, settingsFileBackupPath);
            }

            File.WriteAllText(settingsFilePath, fileContents);
        }
    }
}
