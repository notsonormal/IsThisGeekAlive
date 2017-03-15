using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsThisGeekAlive.ViewModels
{
    public class GeeksIndexViewModel
    {
        public GeeksIndexViewModel()
        {
            Geek = new GeekViewModel();
        }

        public GeekViewModel Geek { get; set; }

        [Display(Name = "Username")]
        [MinLength(5, ErrorMessage = "The username is too short, it must be 5 characters or more")]
        [MaxLength(500, ErrorMessage = "The username is too long, it must be 500 characters or less")])]
        public string ManualLoginUsername { get; set; }

        [Display(Name = "Login code")]
        [MinLength(5, ErrorMessage = "The login code is too short, it must be 5 characters or more")]
        [MaxLength(100, ErrorMessage = "The login code is too long, it must be 100 characters or less")]
        public string ManualLoginCode { get; set; }

        [Display(Name = "Create new geek if not found?")]
        public bool CreateNewGeekIfMissing { get; set; }

        public string Message { get; set; }

        public bool SelectManualLogin { get; set; }

        public short ClientUtcOffset { get; set; }
    }
}
