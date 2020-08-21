using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BetterCommerce.Business.Abstract;
using BetterCommerce.Core.Enums;
using BetterCommerce.Core.Factory;
using BetterCommerce.Core.Utilities.Results;
using BetterCommerce.DataAccess.Abstract;
using BetterCommerce.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;


namespace BetterCommerce.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public ProductManager(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public IDataResult<IQueryable<Product>> GetAllProducts()
        {
            var allProducts = _unitOfWork.GetRepository<Product>().GetAll()
                .Include(x => x.Category)
                .Include(x => x.Images)
                .Include(x => x.ProductDetails)
                .Include(x => x.Brand)
                .Where(x => x.IsDeleted == false);
            return new SuccessDataResult<IQueryable<Product>>(allProducts);
        }


        public IDataResult<IQueryable<Product>> GetProductList(int categoryId, int brandId, int take, int skip,
            int pageSize, double minPrice, double maxPrice, EnumProductListOptions listOptions, string q)
        {
            int pageIndex = skip / take;

            Expression<Func<Product, bool>> where = x => !x.IsDeleted
                                                         && !x.Category.IsDeleted;
            if (categoryId > 0) where = where.And(x => x.CategoryId == categoryId);
            if (brandId > 0) where = where.And(x => x.BrandId == brandId);
            if (minPrice > 0) where = where.And(x => x.Price > minPrice);
            if (maxPrice > 0) where = where.And(x => x.Price < maxPrice);
            if (q != null)
                where = where.And(x => x.Name.ToLower().Contains(q)
                                       || x.Brand.Name.ToLower().Contains(q)
                                       || x.Description.ToLower().Contains(q) 
                                       || x.Category.Name.ToLower().Contains(q));

            var productList = CacheProductList().Data.Where(where);

            switch (listOptions)
            {
                case EnumProductListOptions.Default:
                    productList = productList.Take(take).Skip(skip);
                    break;
                case EnumProductListOptions.FromAToZ:
                    productList = productList.OrderBy(x => x.Name);
                    break;
                case EnumProductListOptions.FromZToA:
                    productList = productList.OrderByDescending(x => x.Name);
                    break;
                case EnumProductListOptions.PriceHighToLow:
                    productList = productList.OrderByDescending(x => x.Price);
                    break;
                case EnumProductListOptions.PriceLowToHigh:
                    productList = productList.OrderBy(x => x.Price);
                    break;
                case EnumProductListOptions.BestStars:
                    productList = productList.OrderByDescending(x => x.Star);
                    break;
                case EnumProductListOptions.BestSellers:
                    productList = productList.OrderByDescending(x => x.SoldCount);
                    break;
                case EnumProductListOptions.NewestProducts:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(listOptions), listOptions, null);
            }

            return new SuccessDataResult<IQueryable<Product>>(productList);
        }

        public IDataResult<IQueryable<Product>> CacheProductList()
        {
            var key = "CachedProductList";
            _cache.TryGetValue(key, out IDataResult<IQueryable<Product>> productList);
            if (productList.Data != null) return productList;

            productList = GetAllProducts();
            var expiry = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(3),
                Priority = CacheItemPriority.Normal
            };
            _cache.Set(key, productList, expiry);
            return productList;
        }

        public IResult AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public IResult DeleteProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public IResult EditProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}