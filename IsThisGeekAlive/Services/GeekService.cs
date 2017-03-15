using IsThisGeekAlive.Data;
using IsThisGeekAlive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsThisGeekAlive.Services
{
    public class GeekService : IGeekService
    {
        readonly GeekDbContext _geekContext;

        public GeekService(GeekDbContext geekContext)
        {
            _geekContext = geekContext;            
        }

        public Geek Login(string username, string loginCode, int notAliveWarningWindow, int notAliveDangerWindow, DateTimeOffset localTime)
        {
            _geekContext.Database.EnsureCreated();

            username = username?.Trim();
            string usernameLower = username?.ToLower();
            DateTimeOffset serverTime = DateTimeOffset.Now;

            var geek = _geekContext.Geeks.SingleOrDefault(x => x.UsernameLower == usernameLower);
            if (geek == null)
            {
                geek = new Geek()
                {
                    Username = username,
                    UsernameLower = usernameLower,
                };

                _geekContext.Geeks.Add(geek);
            }

            geek.LoginCode = loginCode;
            geek.NotAliveWarningWindow = notAliveWarningWindow;
            geek.NotAliveDangerWindow = notAliveDangerWindow;
            geek.LastActivityLocalTime = localTime.UtcDateTime;
            geek.LastActivityLocalTimeUtcOffset = (short)localTime.Offset.TotalMinutes;
            geek.LastActivityServerTime = serverTime.UtcDateTime;
            geek.LastActivityServerTimeUtcOffset = (short)serverTime.Offset.TotalMinutes;

            _geekContext.SaveChanges();

            return geek;
        }

        public Geek GetGeekByUsername(string username)
        {
            _geekContext.Database.EnsureCreated();

            username = username?.Trim();
            string usernameLower = username?.ToLower();

            return _geekContext.Geeks.SingleOrDefault(x => x.UsernameLower == usernameLower);
        }
    }
}
