using BetterCommerce.Business.Abstract;

namespace BetterCommerce.Business.Structure.Abstract
{
    public interface IBusinessService
    {
        public IAuthService Auth { get; }
        public IProductService Product { get; }
        public IRoleService Role { get; }
        public IProductDetailService ProductDetail { get; }
        public IProductImageService ProductImage { get; }

        public ICategoryService Category { get; }
        public IOrderService Order { get; }
        
    }
}