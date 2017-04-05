using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IsThisGeekAlive.Services;
using Microsoft.Extensions.Logging;
using IsThisGeekAlive.Models.Api;
using MySql.Data.MySqlClient;
using System.Net.Http;

namespace IsThisGeekAlive.Controllers
{
    [Produces("application/json")]
    [Route("api/geeks")]
    public class GeeksApiController : Controller
    {
        readonly ILogger _logger;
        readonly IGeekService _geekService;

        public GeeksApiController(ILoggerFactory loggerFactory, IGeekService geekService)
        {
            _logger = loggerFactory.CreateLogger<GeeksApiController>();
            _geekService = geekService;
        }

        protected ILogger Log { get { return _logger; } }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]GeekLogin request)
        {
            if (Log.IsEnabled(LogLevel.Debug))
                Log.LogDebug("POST: /Api/Geeks/Ping");

            try
            {

                _geekService.Login(request.Username, request.LoginCode, request.NotAliveWarningWindow,
                    request.NotAliveDangerWindow, request.LocalTime);
            }
            catch (Exception ex) when (ex is ObjectDisposedException || ex is MySqlException)
            {
                try
                {
                    // The database connection occasionally times out when this is hosted on a free
                    // Microsoft Azure instance. The second request should work
                    _geekService.Login(request.Username, request.LoginCode, request.NotAliveWarningWindow,
                        request.NotAliveDangerWindow, request.LocalTime);
                }
                catch (Exception ex2) when (ex2 is ObjectDisposedException || ex2 is MySqlException)
                {
                    return StatusCode(StatusCodes.Status504GatewayTimeout);
                }
            }

            return Ok();
        }

    }
}