using IsThisGeekAlive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsThisGeekAlive.Services
{
    public interface IGeekService
    {
        Geek Login(string username, int notAliveWarningWindow, int notAliveDangerWindow, DateTimeOffset localTime);
        Geek GetGeekByUsername(string username);
    }
}
