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

namespace IsThisGeekAlive.Controllers
{
    public class GeeksController : Controller
    {
        readonly ILogger _logger;
        readonly IGeekService _geekService;

        public GeeksController(
            ILoggerFactory loggerFactory,
            IGeekService geekService)
        {
            _logger = loggerFactory.CreateLogger<GeeksController>();
            _geekService = geekService;
        }        

        protected ILogger Log { get { return _logger; } }
        
        [HttpGet]
        public IActionResult Index(string username = null)
        {
            if (Log.IsEnabled(LogLevel.Debug))
                Log.LogDebug("GET: /Geeks/Index");

            GeekViewModel geekViewModel = GetGeek(username);

            if (!string.IsNullOrWhiteSpace(username) && geekViewModel.GeekId == 0)
            {
                ModelState.AddModelError("Username", "No geek has been found with that username");
            }

            return View(new GeeksIndexViewModel()
            {
                Geek = geekViewModel
            });
        }

        [HttpPost]
        public IActionResult Search(GeeksIndexViewModel viewModel)
        {
            if (Log.IsEnabled(LogLevel.Debug))
                Log.LogDebug("POST: /Geeks/Search");

            if (string.IsNullOrWhiteSpace(viewModel.Geek.Username))
            {
                ModelState.AddModelError("Username", "The username field is required");
                return View("Index", viewModel);
            }

            return RedirectToAction("Index", new { username = viewModel.Geek.Username });
        }

        GeekViewModel GetGeek(string username)
        {
            GeekViewModel geekViewModel = new GeekViewModel();

            if (username != null)
            {
                var geek = _geekService.GetGeekByUsername(username);

                if (geek != null)
                {
                    geekViewModel = new GeekViewModel(geek);
                }
            }

            return geekViewModel;
        }
    }
}
