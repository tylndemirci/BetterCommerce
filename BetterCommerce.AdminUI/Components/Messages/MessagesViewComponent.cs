using Microsoft.AspNetCore.Mvc;

namespace BetterCommerce.AdminUI.Components.Messages
{
    public class MessagesViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}