using Microsoft.AspNetCore.Mvc;

namespace BetterCommerce.WebUI.Components.ShowCase
{
    public class ShowCaseViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}