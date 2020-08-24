using BetterCommerce.Business.Abstract;
using BetterCommerce.Business.Concrete;
using BetterCommerce.Business.Structure.Abstract;
using BetterCommerce.Core.Identity;
using BetterCommerce.Core.Security.Jwt;
using BetterCommerce.DataAccess.Abstract;
using BetterCommerce.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace BetterCommerce.Business.Structure.Concrete
{
    public class BusinessService : IBusinessService
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IPasswordValidator<ApplicationUser> _passwordValidator;
        private readonly ITokenHelper _tokenHelper;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly IMemoryCache _cache;
        private readonly IBaseDal<Product> _productRepo;
        private readonly IBaseDal<ProductDetail> _productDetailRepo;
        private readonly IBaseDal<ProductImage> _imgRepo;
        private readonly IBaseDal<Category> _categoryRepo;
        private readonly IBaseDal<Order> _orderRepo;
        private readonly IBaseDal<OrderLine> _orderLineRepo;

        public BusinessService(IUnitOfWork uow, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IPasswordValidator<ApplicationUser> passwordValidator,
            ITokenHelper tokenHelper, RoleManager<ApplicationRole> roleManager, IPasswordHasher<ApplicationUser> passwordHasher, IMemoryCache cache, IBaseDal<Product> productRepo,
            IBaseDal<ProductDetail> productDetailRepo, IBaseDal<ProductImage> imgRepo, IBaseDal<Category> categoryRepo, IBaseDal<Order> orderRepo, IBaseDal<OrderLine> orderLineRepo)
        {
            _uow = uow;
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordValidator = passwordValidator;
            _tokenHelper = tokenHelper;
            _roleManager = roleManager;
            _passwordHasher = passwordHasher;
            _cache = cache;
            _productRepo = productRepo;
            _productDetailRepo = productDetailRepo;
            _imgRepo = imgRepo;
            _categoryRepo = categoryRepo;
            _orderRepo = orderRepo;
            _orderLineRepo = orderLineRepo;
        }

        private IAuthService _authService;
        public IAuthService Auth => _authService ??= new AuthManager(_userManager, _signInManager, _passwordValidator, _tokenHelper, _roleManager, _passwordHasher);

        private IProductService _productService;
        public IProductService Product => _productService ??= new ProductManager(_uow, _cache, _productRepo);

        private IRoleService _roleService;
        public IRoleService Role => _roleService ??= new RoleManager(_userManager, _signInManager, _passwordValidator, _tokenHelper, _roleManager, _passwordHasher);

        private IProductDetailService _productDetailService;
        public IProductDetailService ProductDetail => _productDetailService ??= new ProductDetailManager(_productDetailRepo, _uow);

        private IProductImageService _productImageService;
        public IProductImageService ProductImage => _productImageService ??= new ProductImageManager(_imgRepo, _uow);

        private ICategoryService _categoryService;
        public ICategoryService Category => _categoryService ??= new CategoryManager(_categoryRepo, _uow);
        
        private IOrderService _orderService;
        public IOrderService Order => _orderService ??= new OrderManager(_orderRepo);
    }
}