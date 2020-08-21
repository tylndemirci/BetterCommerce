using Microsoft.AspNetCore.Mvc;

namespace BetterCommerce.WebUI.Components.Carousel
{
    public class CarouselViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}