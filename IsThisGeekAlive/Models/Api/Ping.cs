using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsThisGeekAlive.Models.Api
{
    public class Ping
    {
        public string Username { get; set; }        
        public int NotAliveWarningWindow { get; set; }
        public int NotAliveDangerWindow { get; set; }
        public DateTimeOffset LocalTime { get; set; }
    }
}
