using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IsThisGeekAliveMonitor.Utils
{
    public static class ProjectUtils
    {
        public static string GetProjectName()
        {
            return Assembly.GetEntryAssembly().GetName().Name;
        }

        public static string GetProjectExe()
        {
            return Assembly.GetEntryAssembly().Location;
        }

        public static void ToggleRunOnWindowsStartup(bool runOnStartup)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (runOnStartup)
            {
                registryKey.SetValue("IsThisGeekAliveMonitor", GetProjectExe());
            }
            else
            {
                registryKey.DeleteValue("AstoundingDock", false);
            }
        }
    }
}
