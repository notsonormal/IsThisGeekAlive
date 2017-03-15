using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsThisGeekAliveMonitor.Models
{
    public class GeekLogin
    {
        public string Username { get; set; }
        public string LoginCode { get; set; }
        public int NotAliveWarningWindow { get; set; }
        public int NotAliveDangerWindow { get; set; }
        public DateTimeOffset LocalTime { get; set; }
    }
}
