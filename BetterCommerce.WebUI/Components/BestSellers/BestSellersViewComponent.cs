using Microsoft.AspNetCore.Mvc;

namespace BetterCommerce.WebUI.Components.BestSellers
{
    public class BestSellersViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }   
    }
}