using FlightMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightMVC.Components
{
    public class MenuViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(int val)
        {
            Preferences p = new Preferences() { Food = $"I want {val} slices...", ExtraLegRoom = (val % 2==0) };
            return View(p);
        }
    }
}
