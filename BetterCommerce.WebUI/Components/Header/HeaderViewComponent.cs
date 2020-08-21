using Microsoft.AspNetCore.Mvc;

namespace BetterCommerce.WebUI.Components.Header
{
    public class HeaderViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}