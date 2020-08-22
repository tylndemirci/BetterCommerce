using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BetterCommerce.Business.Abstract;
using BetterCommerce.Core.Enums;
using BetterCommerce.Core.Extensions;
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
        private readonly IBaseDal<Product> _productRepo;

        public ProductManager(IUnitOfWork unitOfWork, IMemoryCache cache, IBaseDal<Product> baseDal)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
            _productRepo = baseDal;
        }

        public IDataResult<IQueryable<Product>> GetAllProducts()
        {
            var allProducts = _productRepo.GetAll()
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
            if (minPrice > 0) where = where.And(x => x.FinalPrice > minPrice);
            if (maxPrice > 0) where = where.And(x => x.FinalPrice < maxPrice);
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
                    productList = productList.OrderByDescending(x => x.FinalPrice);
                    break;
                case EnumProductListOptions.PriceLowToHigh:
                    productList = productList.OrderBy(x => x.FinalPrice);
                    break;
                case EnumProductListOptions.BestStars:
                    productList = productList.OrderByDescending(x => x.Star);
                    break;
                case EnumProductListOptions.BestSellers:
                    productList = productList.OrderByDescending(x => x.SoldCount);
                    break;
                case EnumProductListOptions.NewestProducts:
                    productList = productList.OrderBy(x => x.CreatedAt);
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
            if (product == null) return new ErrorResult("Product is empty.");
            if (product.Name.IsNullS()) return new ErrorResult("Product name is empty.");
            var newProduct = new Product
            {
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                Count = product.Count,
                Description = product.Description,
                Name = product.Name.FirstCharToUpper(),
                BasePrice = product.BasePrice,
                DiscountPrice = product.DiscountPrice,
                FinalPrice = product.BasePrice - product.DiscountPrice,
                IsFeatured = product.IsFeatured,
                IsStock = product.Count > 0,
                IsInSale = product.DiscountPrice > 0,
                CreatedAt = DateTime.Now
            };
            _productRepo.Create(newProduct);
            var result = _unitOfWork.SaveChanges();
            if (result == 0)
            {
                return new ErrorResult("Failed to add the product.");
            }

            return new SuccessResult("Product successfully added.");
        }

        public IResult DeleteProduct(Product product)
        {
            var deletingProduct = _productRepo.GetBy(x => x.Id == product.Id).FirstOrDefault();
            if (deletingProduct == null) return new ErrorResult("Product not found.");
            deletingProduct.BrandId = product.BrandId;
            deletingProduct.CategoryId = product.CategoryId;
            deletingProduct.Count = product.Count;
            deletingProduct.Description = product.Description;
            deletingProduct.Name = product.Name.FirstCharToUpper();
            deletingProduct.BasePrice = product.BasePrice;
            deletingProduct.DiscountPrice = product.DiscountPrice;
            deletingProduct.FinalPrice = product.BasePrice - product.DiscountPrice;
            deletingProduct.IsFeatured = product.IsFeatured;
            deletingProduct.IsStock = product.Count > 0;
            deletingProduct.IsInSale = product.DiscountPrice > 0;
            deletingProduct.Star = product.Star;
            deletingProduct.SoldCount = product.SoldCount;
            deletingProduct.ModifiedAt = DateTime.Now;
            deletingProduct.CreatedAt = product.CreatedAt;
            deletingProduct.IsDeleted = true;
            _productRepo.Update(deletingProduct);
            var result = _unitOfWork.SaveChanges();
            if (result == 0)
            {
                return new ErrorResult("Failed to update the product.");
            }

            return new SuccessResult("Product successfully deleted.");
        }

        public IResult EditProduct(Product product)
        {
            if (product == null) return new ErrorResult("Product is empty.");
            if (product.Name.IsNullS()) return new ErrorResult("Product name is empty.");
            var newProduct = _productRepo.GetBy(x => x.Id == product.Id).FirstOrDefault();
            if (newProduct != null)
            {
                newProduct.BrandId = product.BrandId;
                newProduct.CategoryId = product.CategoryId;
                newProduct.Count = product.Count;
                newProduct.Description = product.Description;
                newProduct.Name = product.Name.FirstCharToUpper();
                newProduct.BasePrice = product.BasePrice;
                newProduct.DiscountPrice = product.DiscountPrice;
                newProduct.FinalPrice = product.BasePrice - product.DiscountPrice;
                newProduct.IsFeatured = product.IsFeatured;
                newProduct.IsStock = product.Count > 0;
                newProduct.IsInSale = product.DiscountPrice > 0;
                newProduct.Star = product.Star;
                newProduct.SoldCount = product.SoldCount;
                newProduct.ModifiedAt = DateTime.Now;
                newProduct.CreatedAt = product.CreatedAt;
                _productRepo.Create(newProduct);
            }

            var result = _unitOfWork.SaveChanges();
            if (result == 0)
            {
                return new ErrorResult("Failed to add the product.");
            }

            return new SuccessResult("Product successfully added.");
        }
    }
}