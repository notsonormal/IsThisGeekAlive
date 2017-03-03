using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsThisGeekAlive.Models
{
    public class Geek
    {
        public int GeekId { get; set; }
        public string Username { get; set; }
        public string UsernameLower { get; set; }
        public int NotAliveWarningWindow { get; set; }
        public int NotAliveDangerWindow { get; set; }
        public DateTimeOffset LastPingLocalTime { get; set; }
        public DateTimeOffset LastPingServerTime { get; set; }
    }
}
