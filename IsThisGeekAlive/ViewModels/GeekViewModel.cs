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
            LoginCode = geek.LoginCode;
            NotAliveWarningWindow = geek.NotAliveWarningWindow;
            NotAliveDangerWindow = geek.NotAliveDangerWindow;

            LastActivityLocalTime = new DateTimeOffset(
                geek.LastActivityLocalTime,
                TimeSpan.FromMinutes(geek.LastActivityLocalTimeUtcOffset));

            LastActivityServerTime = new DateTimeOffset(
                geek.LastActivityServerTime,
                TimeSpan.FromMinutes(geek.LastActivityServerTimeUtcOffset));
        }

        public int GeekId { get; set; }    
        public string Username { get; set; }
        public string UsernameLower { get; set; }        
        public string LoginCode { get; set; }
        public int NotAliveWarningWindow { get; set; }
        public int NotAliveDangerWindow { get; set; }        

        [DisplayFormat(DataFormatString = "{0:f} ({0:zzz})", ApplyFormatInEditMode = true)]
        public DateTimeOffset LastActivityLocalTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:f} ({0:zzz})", ApplyFormatInEditMode = true)]
        public DateTimeOffset LastActivityServerTime { get; set; }
       

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
        /// e.g. 1 day(s) and 3 hour(s) ago
        /// </summary>
        /// <returns></returns>
        public string LastPingDaysAndHoursAgo()
        {
            TimeSpan since = GetTimeSince();
            return string.Format("{0} day(s) and {1} hour(s) ago", since.Days, since.Hours);
        }

        public bool ShowActivityDates()
        {
            return AlwaysShowPingTimes || HasHitDangerThreshold() || HasHitWarningThreshold();
        }

        TimeSpan GetTimeSince()
        {
            return DateTime.UtcNow - LastActivityServerTime.ToUniversalTime();
        }
    }
}
