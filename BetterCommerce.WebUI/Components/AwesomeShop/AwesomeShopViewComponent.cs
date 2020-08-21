using Microsoft.AspNetCore.Mvc;

namespace BetterCommerce.WebUI.Components.AwesomeShop
{
    public class AwesomeShopViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}