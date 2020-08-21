using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BetterCommerce.WebUI.Controllers
{
    public class HomeController : BaseController
    {
        
        public IActionResult Index()
        {
            return View();
        }



    }
}