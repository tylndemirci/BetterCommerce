using Microsoft.AspNetCore.Mvc;

namespace BetterCommerce.AdminUI.Components.CommentAndChat
{
    public class CommentAndChatViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}