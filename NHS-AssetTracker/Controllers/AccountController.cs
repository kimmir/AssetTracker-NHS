using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NHS_AssetTracker.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View(); //26.03.20 - Athar: Index Page for Account
        }

        public IActionResult Login()
        {
            return View(); //26.03.20 - Athar: Login Page for Account
        }

        public IActionResult Register()
        {
            return View(); //26.03.20 - Athar: Register Page for Account
        }

        public IActionResult Profile()
        {
            return View(); //26.03.20 - Athar: Profile Page for Account
        }
    }
}