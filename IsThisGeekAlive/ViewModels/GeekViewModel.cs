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

            LastActivityLocalTime = geek.LastActivityLocalTime;
            LastActivityServerTime = geek.LastActivityServerTime;
        }

        public int GeekId { get; set; }
        public string Username { get; set; }
        public string UsernameLower { get; set; }
        public int NotAliveWarningWindow { get; set; }
        public int NotAliveDangerWindow { get; set; }

        [DisplayFormat(DataFormatString = "{0:f} ({0:zzz})", ApplyFormatInEditMode = true)]
        public DateTimeOffset LastActivityLocalTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:f} ({0:zzz})", ApplyFormatInEditMode = true)]
        public DateTimeOffset LastActivityServerTime { get; set; }
        
        public DateTimeOffset LastPingServerTimeInUtc
        {
            get { return LastActivityServerTime.ToUniversalTime(); }
        }

        public bool DoesGeekExist { get; set; }
        public bool AlwaysShowPingTimes { get; set; }

        public bool HasHitDangerThreshold()
        {            
            TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Hawaiian Standard Time"));

            TimeSpan since = GetTimeSince();
            return since.TotalHours > NotAliveDangerWindow;
        }

        public bool HasHitWarningThreshold()
        {
            TimeSpan since = GetTimeSince();
            return since.TotalHours > NotAliveWarningWindow;
        }

        /// <summary>
        /// e.g. 1 days and 3 hours ago
        /// </summary>
        /// <returns></returns>
        public string LastPingDaysAndHoursAgo()
        {
            TimeSpan since = GetTimeSince();
            return string.Format("{0} days and {1} hours ago", since.Days, since.Hours);
        }

        public bool ShowActivityDates()
        {
            return AlwaysShowPingTimes || HasHitDangerThreshold() || HasHitWarningThreshold();
        }

        TimeSpan GetTimeSince()
        {
            return DateTime.UtcNow - LastPingServerTimeInUtc;
        }
    }
}
