using System.Collections.Generic;
using System.Linq;
using BetterCommerce.Core.Utilities.Results;
using BetterCommerce.Entity.Entities;

namespace BetterCommerce.Business.Abstract
{
    public interface IProductDetailService
    {
        IDataResult<IQueryable<ProductDetail>> GetProductDetails(int productId);
        IResult AddProductDetail(List<ProductDetail> productDetail);
        IResult DeleteProductDetail(List<ProductDetail> productDetail);
        IResult EditProductDetail(List<ProductDetail> productDetail);
    }
}    