
using BetterCommerce.Business.Abstract;
using BetterCommerce.Core.Identity;
using BetterCommerce.DataAccess.Abstract;
using BetterCommerce.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BetterCommerce.AdminUI.Controllers
{
    public class BaseController: Controller
    {
        public readonly IAuthService _AuthService;
        public readonly ICategoryService _CategoryService;
        public readonly IOrderService _OrderService;
        public readonly IProductDetailService _ProductDetailService;
        public readonly IProductImageService _ProductImageService;
        public readonly IProductService _ProductService;
        public readonly IRoleService _RoleService;
        public readonly UserManager<ApplicationUser> _UserManager;
        public readonly IBaseDal<Order> _OrderBaseDal;
        public readonly IBaseDal<OrderLine> _OrderLineBaseDal;

        public BaseController(IAuthService authService, ICategoryService categoryService, IOrderService orderService, IProductDetailService productDetailService, IProductImageService productImageService, IProductService productService, IRoleService roleService, UserManager<ApplicationUser> userManager, IBaseDal<Order> orderBaseDal, IBaseDal<OrderLine> orderLineBaseDal)
        {
            _AuthService = authService;
            _CategoryService = categoryService;
            _OrderService = orderService;
            _ProductDetailService = productDetailService;
            _ProductImageService = productImageService;
            _ProductService = productService;
            _RoleService = roleService;
            _UserManager = userManager;
            _OrderBaseDal = orderBaseDal;
            _OrderLineBaseDal = orderLineBaseDal;
        }
    }
}