using Microsoft.AspNetCore.Mvc;

namespace BetterCommerce.AdminUI.Components.UserProfileAndSearch
{
    public class UserProfileAndSearchViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}