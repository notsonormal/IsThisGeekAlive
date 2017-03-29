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
            if (geek != null)
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

                DoesGeekExist = true;
            }
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
            TimeSpan since = GetTimeSince();
            return since.TotalHours > NotAliveDangerWindow;
        }

        public bool HasHitWarningThreshold()
        {
            TimeSpan since = GetTimeSince();
            return since.TotalHours > NotAliveWarningWindow;
        }

        /// <summary>
        /// e.g. 1 day and 3 hours
        /// </summary>
        /// <returns></returns>
        public string LastPingDaysAndHoursAgo()
        {
            TimeSpan since = GetTimeSince();
            string days = since.Days == 1 ? "day" : "days";
            string hours = since.Hours == 1 ? "hour" : "hours";

            return $"{since.Days} {days} and {since.Hours} {hours}";
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
