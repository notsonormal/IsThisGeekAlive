using IsThisGeekAlive.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsThisGeekAlive.ViewModels
{
    public class GeekViewModel
    {
        public GeekViewModel()
        {

        }

        public GeekViewModel(Geek geek)
        {
            GeekId = geek.GeekId;
            Username = geek.Username;
            UsernameLower = geek.UsernameLower;
            NotAliveWarningWindow = geek.NotAliveWarningWindow;
            NotAliveDangerWindow = geek.NotAliveDangerWindow;

            LastPingLocalTime = geek.LastPingLocalTime;
            LastPingServerTime = geek.LastPingServerTime;

            DoesGeekExist = true;
        }

        public int GeekId { get; set; }
        public string Username { get; set; }
        public string UsernameLower { get; set; }
        public int NotAliveWarningWindow { get; set; }
        public int NotAliveDangerWindow { get; set; }

        [DisplayFormat(DataFormatString = "{0:f}")]
        public DateTimeOffset LastPingLocalTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:f}")]
        public DateTimeOffset LastPingServerTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:f}")]
        public DateTimeOffset LastPingServerTimeInUtc
        {
            get { return LastPingServerTime.ToUniversalTime(); }
        }

        public bool DoesGeekExist { get; set; }

        public bool HasHitDangerThreshold()
        {
            TimeSpan since = DateTime.UtcNow - LastPingServerTimeInUtc;
            return since.TotalHours > NotAliveDangerWindow;
        }

        public bool HasHitWarningThreshold()
        {
            TimeSpan since = DateTime.UtcNow - LastPingServerTimeInUtc;
            return since.TotalHours > NotAliveWarningWindow;
        }

        public string LastPingDaysAndHoursAgo()
        {
            TimeSpan since = DateTime.UtcNow - LastPingServerTimeInUtc;
            return string.Format("{0} days and {1} hours ago", since.Days, since.Hours);
        }
    }
}
