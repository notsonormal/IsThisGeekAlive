using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using IsThisGeekAlive.Utils;

namespace IsThisGeekAlive.Controllers
{
    public class HomeController : Controller
    {
        readonly AppSettings _appSettings;

        public HomeController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
