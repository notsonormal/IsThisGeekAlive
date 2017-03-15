using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsThisGeekAlive.Models
{
    public class Geek
    {
        public const int DefaultNotAliveWarningWindow = 36;
        public const int DefaultNotAliveDangerWindow = 96;

        public int GeekId { get; set; }
        public string Username { get; set; }
        public string UsernameLower { get; set; }
        public string LoginCode { get; set; }
        public int NotAliveWarningWindow { get; set; }
        public int NotAliveDangerWindow { get; set; }
        public DateTime LastActivityLocalTime { get; set; }
        public short LastActivityLocalTimeUtcOffset { get; set; }
        public DateTime LastActivityServerTime { get; set; }
        public short LastActivityServerTimeUtcOffset { get; set; }
    }
}
