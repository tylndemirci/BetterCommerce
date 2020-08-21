using Microsoft.AspNetCore.Mvc;

namespace BetterCommerce.AdminUI.Components.BreadCrumbAndRightSideBar
{
    public class BreadCrumbAndRightSideBarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}