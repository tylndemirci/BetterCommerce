using Microsoft.AspNetCore.Mvc;

namespace BetterCommerce.AdminUI.Components.SalesCards
{
    public class SalesCardsViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}