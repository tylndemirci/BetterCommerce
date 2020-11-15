using System.Linq;
using BetterCommerce.Business.Abstract;
using BetterCommerce.Core.Identity;
using BetterCommerce.DataAccess.Abstract;
using BetterCommerce.Entity.Entities;
using BetterCommerce.Entity.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace BetterCommerce.AdminUI.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public int ReturnUserCount()
        {
            var userCount = _UserManager.Users.Count();
            return userCount;
        }

        public int ReturnNewUserCount()
        {
            var newUserCount = _UserManager.Users.Count(x => x.CreatedAt.Date < x.CreatedAt.AddDays(7));
            return newUserCount;
        }

        public int TotalShopCount()
        {
            var totalShopCount = _OrderBaseDal.GetAll().Count();
            return totalShopCount;
        }

        public int TotalOrderCount()
        {
            var totalOrderCount = _OrderLineBaseDal.GetAll().Sum(x=>x.Quantity);
            return totalOrderCount;
        }

        public int PendingOrderCount()
        {
            var pendingOrderCount = _OrderLineBaseDal.GetAll().Count(x => x.Status == EnumOrderLineStatus.NotDelivered);
            return pendingOrderCount;
        }

        public int ReportCount()
        {
            return -1;
        }


        public HomeController(IAuthService authService, ICategoryService categoryService, IOrderService orderService,
            IProductDetailService productDetailService, IProductImageService productImageService,
            IProductService productService, IRoleService roleService, UserManager<ApplicationUser> userManager,
            IBaseDal<Order> orderBaseDal, IBaseDal<OrderLine> orderLineBaseDal)
            : base(authService, categoryService, orderService, productDetailService, productImageService,
                productService, roleService, userManager, orderBaseDal, orderLineBaseDal)
        {
        }
    }
}