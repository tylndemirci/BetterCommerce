using System.Collections.Generic;
using System.Linq;
using BetterCommerce.Core.Utilities.Results;
using BetterCommerce.Entity.Entities;

namespace BetterCommerce.Business.Abstract
{
    public interface IProductImageService
    {
        IDataResult<IQueryable<ProductImage>> GetImagesOfProduct(int productId);
        IResult AddProductImage(List<ProductImage> productImage);
        IResult DeleteProductImage(ProductImage productImage);
    }
}