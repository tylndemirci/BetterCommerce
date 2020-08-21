using Microsoft.AspNetCore.Mvc;


namespace BetterCommerce.AdminUI.Controllers
{
    public class HomeController : Controller
    {
      

        public IActionResult Index()
        {
            return View();
        }
 
      
    }
}