using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsThisGeekAliveMonitor.Utils
{
    public static class ProjectUtils
    {
        public static string GetProjectName()
        {
            return System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
        }
    }
}
