using System.Linq;
using BetterCommerce.Core.Utilities.Results;
using BetterCommerce.Entity.Entities;
using BetterCommerce.Entity.Enums;

namespace BetterCommerce.Business.Abstract
{
    public interface IProductService
    {
        IDataResult<IQueryable<Product>> GetAllProducts();
        IDataResult<IQueryable<Product>> GetProductList(int categoryId, int brandId, int take, int skip, int pageSize, double minPrice, double maxPrice, EnumProductListOptions listOptions, string q);
        IDataResult<IQueryable<Product>> CacheProductList();
        IResult AddProduct(Product product);
        IResult DeleteProduct(Product product);
        IResult EditProduct(Product product);
       
    }    
}