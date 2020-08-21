using Microsoft.AspNetCore.Mvc;

namespace BetterCommerce.WebUI.Components.Footer
{
    public class FooterViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}