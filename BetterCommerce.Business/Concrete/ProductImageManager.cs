using System;
using System.Collections.Generic;
using System.Linq;
using BetterCommerce.Business.Abstract;
using BetterCommerce.Core.Utilities.Results;
using BetterCommerce.DataAccess.Abstract;
using BetterCommerce.Entity.Entities;

namespace BetterCommerce.Business.Concrete
{
    public class ProductImageManager:IProductImageService
    {
        private readonly IBaseDal<ProductImage> _imgRepo;
        private readonly IUnitOfWork _unitOfWork;

        public ProductImageManager(IBaseDal<ProductImage> imgRepo, IUnitOfWork unitOfWork)
        {
            _imgRepo = imgRepo;
            _unitOfWork = unitOfWork;
        }

        public IDataResult<IQueryable<ProductImage>> GetImagesOfProduct(int productId)
        {
            if (productId==0) return new ErrorDataResult<IQueryable<ProductImage>>("Product not found.");
            var productImages = _imgRepo.GetBy(x => x.ProductId == productId);
            if (productImages==null) return new ErrorDataResult<IQueryable<ProductImage>>("There is no image for this product.");
            return new SuccessDataResult<IQueryable<ProductImage>>(productImages);
        }

        public IResult AddProductImage(List<ProductImage> productImage)
        {
            
            if (productImage==null) return new ErrorResult("There is no image.");
            foreach (var image in productImage)
            {
                if (image.ImageUrl==null) return new ErrorResult("There is no image");
                var newImage = new ProductImage();
                newImage.ImageUrl = image.ImageUrl;
                newImage.ProductId = image.ProductId;
                newImage.CreatedAt = DateTime.Now;
                _imgRepo.Create(newImage);
            }
            var result = _unitOfWork.SaveChanges();
            if (result==0)
            {
                return new ErrorResult("Image not saved.");
            }
            return new SuccessResult("Image successfully saved.");

        }

        public IResult DeleteProductImage(ProductImage productImage)
        {
            if (productImage==null) return new ErrorResult("There is no image.");
            var deletingImage = _imgRepo.GetBy(x => x.Id == productImage.Id);
            productImage.IsDeleted = true;
            productImage.ModifiedAt = DateTime.Now;
            _imgRepo.Update(productImage);
            var result = _unitOfWork.SaveChanges();
            if (result==0)
            {
                return new ErrorResult("Image not deleted.");
            }
            return new SuccessResult("Image successfully deleted.");
            
        }
    }
}