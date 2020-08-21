using Microsoft.AspNetCore.Mvc;

namespace BetterCommerce.AdminUI.Components.SalesChart
{
    public class SalesChartViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}