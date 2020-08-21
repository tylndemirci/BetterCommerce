using Microsoft.AspNetCore.Mvc;

namespace BetterCommerce.AdminUI.Components.Comment
{
    public class CommentViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}