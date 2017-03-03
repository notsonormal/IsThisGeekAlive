using System;
using System.Collections.Generic;
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
    }
}
