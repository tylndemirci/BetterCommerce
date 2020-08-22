using System;
using System.Collections.Generic;
using System.Linq;
using BetterCommerce.Business.Abstract;
using BetterCommerce.Core.Extensions;
using BetterCommerce.Core.Utilities.Results;
using BetterCommerce.DataAccess.Abstract;
using BetterCommerce.Entity.Entities;

namespace BetterCommerce.Business.Concrete
{
    public class ProductDetailManager : IProductDetailService
    {
        private readonly IUnitOfWork _uow;
        private readonly IBaseDal<ProductDetail> _productDetailRepo;

        public ProductDetailManager(IBaseDal<ProductDetail> productDetailRepo, IUnitOfWork unitOfWork)
        {
            _productDetailRepo = productDetailRepo;
            _uow = unitOfWork;
        }

        public IDataResult<IQueryable<ProductDetail>> GetProductDetails(int productId)
        {
            var productDetails = _productDetailRepo.GetBy(x => x.ProductId == productId)?
                .Where(x => x.IsDeleted == false);
            if (productDetails == null) return new ErrorDataResult<IQueryable<ProductDetail>>("There is not any product detail for this product.");
            return new SuccessDataResult<IQueryable<ProductDetail>>(productDetails);
        }

        public IResult AddProductDetail(List<ProductDetail> productDetail)
        {
            if (productDetail == null) return new ErrorResult("Product detail is empty.");
            if (productDetail[0].ProductId == 0) return new ErrorResult("Product not found.");
            foreach (var detail in productDetail)
            {
                if (detail.Title.IsNullS()) return new ErrorResult("Title is empty.");
                if (detail.Description.IsNullS()) return new ErrorResult("Description is empty.");
                var addingDetail = new ProductDetail();
                addingDetail.Title = detail.Title.FirstCharToUpper();
                addingDetail.Description = detail.Description.FirstCharToUpper();
                addingDetail.ProductId = detail.ProductId;
                addingDetail.CreatedAt = DateTime.Now;
                _productDetailRepo.Create(addingDetail);
            }

            var result = _uow.SaveChanges();
            if (result == 0)
            {
                return new ErrorResult("Failed to add product detail.");
            }

            return new SuccessResult("Product detail successfully added.");
        }

        public IResult DeleteProductDetail(List<ProductDetail> productDetail)
        {
            if (productDetail == null) return new ErrorResult("Product detail is empty.");
            if (productDetail[0].ProductId == 0) return new ErrorResult("Product not found.");
            foreach (var detail in productDetail)
            {
                var dbDetail = _productDetailRepo.GetBy(x => x.Id == detail.Id).FirstOrDefault();
                if (dbDetail == null) return new ErrorResult("Product detail not found.");
                dbDetail.Description = detail.Description.FirstCharToUpper();
                dbDetail.Title = detail.Title.FirstCharToUpper();
                dbDetail.ProductId = detail.ProductId;
                dbDetail.CreatedAt = detail.CreatedAt;
                detail.IsDeleted = true;
                detail.ModifiedAt = DateTime.Now;
                _productDetailRepo.Update(detail);
            }

            var result = _uow.SaveChanges();
            if (result == 0)
            {
                return new ErrorResult("Failed to delete product detail.");
            }

            return new SuccessResult("Product detail successfully deleted.");
        }

        public IResult EditProductDetail(List<ProductDetail> productDetail)
        {
            if (productDetail == null) return new ErrorResult("Product detail is empty.");
            if (productDetail[0].ProductId == 0) return new ErrorResult("Product not found.");

            foreach (var detail in productDetail)
            {
                var dbDetail = _productDetailRepo.GetBy(x => x.Id == detail.Id).FirstOrDefault();
                if (dbDetail == null) return new ErrorResult("Product detail not found.");
                dbDetail.Description = detail.Description.FirstCharToUpper();
                dbDetail.Title = detail.Title.FirstCharToUpper();
                dbDetail.ProductId = detail.ProductId;
                dbDetail.CreatedAt = detail.CreatedAt;
                dbDetail.ModifiedAt = DateTime.Now;
                _productDetailRepo.Update(dbDetail);
            }

            var result = _uow.SaveChanges();
            if (result == 0)
            {
                return new ErrorResult("Failed to delete product detail.");
            }

            return new SuccessResult("Product detail successfully deleted.");
        }
    }
}