using Microsoft.AspNetCore.Mvc;

namespace BetterCommerce.WebUI.Components.Featured
{
    public class FeaturedViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}