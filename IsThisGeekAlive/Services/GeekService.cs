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

        public Geek PingGeek(string username, int notAliveWarningWindow, int notAliveDangerWindow, DateTimeOffset localTime)
        {
            username = username?.Trim();
            string usernameLower = username?.ToLower();

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

            geek.NotAliveWarningWindow = notAliveWarningWindow;
            geek.NotAliveDangerWindow = notAliveDangerWindow;
            geek.LastPingLocalTime = localTime;
            geek.LastPingServerTime = DateTimeOffset.Now;

            _geekContext.SaveChanges();

            return geek;
        }

        public Geek GetGeekByUsername(string username)
        {
            username = username?.Trim();
            string usernameLower = username?.ToLower();

            return _geekContext.Geeks.SingleOrDefault(x => x.UsernameLower == usernameLower);
        }
    }
}
