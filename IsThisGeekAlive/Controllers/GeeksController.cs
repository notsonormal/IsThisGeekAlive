using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using IsThisGeekAlive.Models;
using IsThisGeekAlive.Services;
using IsThisGeekAlive.ViewModels;
using IsThisGeekAlive.Utils;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace IsThisGeekAlive.Controllers
{
    public class GeeksController : Controller
    {
        readonly ILogger _logger;
        readonly IGeekService _geekService;
        readonly AppSettings _appSettings;

        public GeeksController(
            ILoggerFactory loggerFactory,
            IOptions<AppSettings> appSettings,
            IGeekService geekService)
        {
            _logger = loggerFactory.CreateLogger<GeeksController>();
            _geekService = geekService;
            _appSettings = appSettings.Value;
        }       

        protected ILogger Log { get { return _logger; } }
        
        [HttpGet]
        public IActionResult Index(string username = null, string manualLoginUsername = null, 
            string manualLoginCode = null, string message = null)
        {
            if (Log.IsEnabled(LogLevel.Debug))
                Log.LogDebug("GET: /Geeks/Index");

            TimeZoneInfo gmtTimeZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");

            var viewModel = new GeeksIndexViewModel()
            {
                ManualLoginUsername = manualLoginUsername,
                ManualLoginCode = manualLoginCode,
                Message = message,
                IsGmtInDaylightSaving = gmtTimeZone.IsDaylightSavingTime(DateTime.UtcNow)
            };

            GeekViewModel geekViewModel = GetGeek(username);
            viewModel.Geek = geekViewModel;

            if (!string.IsNullOrWhiteSpace(username) && !geekViewModel.DoesGeekExist)
            {
                ModelState.AddModelError("Geek.Username", "No geek has been found with that username");
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Search(GeeksIndexViewModel viewModel)
        {
            if (Log.IsEnabled(LogLevel.Debug))
                Log.LogDebug("POST: /Geeks/Search");

            if (string.IsNullOrWhiteSpace(viewModel.Geek.Username))
            {
                ModelState.AddModelError("Geek.Username", "The username field is required");
                return View("Index", viewModel);
            }

            return RedirectToAction("Index", new { username = viewModel.Geek.Username });
        }

        [HttpPost]
        public IActionResult ManualLogin(GeeksIndexViewModel viewModel)
        {
            if (Log.IsEnabled(LogLevel.Debug))
                Log.LogDebug("POST: /Geeks/ManualLogin");

            bool isValid = ValidateManualLogin(viewModel);
            if (!isValid)
            {
                return View("Index", viewModel);
            }

            string successMessage =
                $"Login was successful with username {viewModel.ManualLoginUsername} " +
                $"and login code {viewModel.ManualLoginCode}";

            var geek = GetGeek(viewModel.ManualLoginUsername);

            var localTime = new DateTimeOffset(DateTime.UtcNow).ToOffset(
                TimeSpan.FromMinutes(viewModel.ClientUtcOffset));

            if (geek.DoesGeekExist)
            {
                if (geek.LoginCode != viewModel.ManualLoginCode)
                {
                    ModelState.AddModelError("ManualLoginCode", "The login code must match the previously set login code");
                    return View("Index", viewModel);
                }

                _geekService.Login(geek.Username, geek.LoginCode, geek.NotAliveWarningWindow,
                    geek.NotAliveDangerWindow, localTime);
            }
            else
            {
                if (!viewModel.CreateNewGeekIfMissing)
                {
                    ModelState.AddModelError("ManualLoginUsername", "No geek has been found with that username");
                    return View("Index", viewModel);
                }

                _geekService.Login(viewModel.ManualLoginUsername, viewModel.ManualLoginCode,
                    Geek.DefaultNotAliveWarningWindow, Geek.DefaultNotAliveDangerWindow, localTime);
            }

            return RedirectToAction("Index", new { message = successMessage });
        }

        bool ValidateManualLogin(GeeksIndexViewModel viewModel)
        {
            if (string.IsNullOrWhiteSpace(viewModel.ManualLoginUsername))
            {
                ModelState.AddModelError("ManualLoginUsername", "The manual login username field is required");
                return false;
            }

            if (string.IsNullOrWhiteSpace(viewModel.ManualLoginCode))
            {
                ModelState.AddModelError("ManualLoginCode", "The manual login code field is required");
                return false;
            }

            return true;
        }

        GeekViewModel GetGeek(string username)
        {
            GeekViewModel geekViewModel = new GeekViewModel();

            if (username != null)
            {
                try
                {
                    var geek = _geekService.GetGeekByUsername(username);
                    geekViewModel = new GeekViewModel(geek);
                }
                catch (Exception ex) when (ex is ObjectDisposedException || ex is MySqlException)
                {
                    // The database connection occasionally times out when this is hosted on a free
                    // Microsoft Azure instance. The second request should work and this saves 
                    // us having to ask the user to reload the page

                    string errorMessage =
                        "Got exception when trying to get the geek from the database. " +
                        "Assuming it's just a timeout and repeating the database call, {0}";

                    Log.LogError(errorMessage, ex);

                    var geek = _geekService.GetGeekByUsername(username);
                    geekViewModel = new GeekViewModel(geek);
                }
            }

            geekViewModel.AlwaysShowPingTimes = _appSettings.AlwaysShowPingTimes;

            return geekViewModel;
        }
    }
}
