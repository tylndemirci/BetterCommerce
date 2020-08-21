using Microsoft.AspNetCore.Mvc;

namespace BetterCommerce.AdminUI.Components.LeftSideBar
{
    public class LeftSideBarViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}