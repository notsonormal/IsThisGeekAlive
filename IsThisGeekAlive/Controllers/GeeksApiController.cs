using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IsThisGeekAlive.Services;
using Microsoft.Extensions.Logging;
using IsThisGeekAlive.Models.Api;

namespace IsThisGeekAlive.Controllers
{
    [Produces("application/json")]
    [Route("Api/Geeks")]
    public class GeeksApiController : Controller
    {
        readonly ILogger _logger;
        readonly IGeekService _geekService;

        public GeeksApiController(
            ILoggerFactory loggerFactory,
            IGeekService geekService)
        {
            _logger = loggerFactory.CreateLogger<GeeksApiController>();
            _geekService = geekService;
        }

        protected ILogger Log { get { return _logger; } }

        [HttpPost]
        public PingResult Ping([FromBody]Ping request)
        {
            if (Log.IsEnabled(LogLevel.Debug))
                Log.LogDebug("POST: /Api/Geeks/Ping");

            _geekService.PingGeek(request.Username, request.NotAliveWarningWindow,
                request.NotAliveDangerWindow, request.LocalTime);

            return new PingResult()
            {

            };
        }

    }
}