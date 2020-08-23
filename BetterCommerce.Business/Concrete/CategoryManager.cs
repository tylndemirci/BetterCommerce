using System;
using System.Linq;
using BetterCommerce.Business.Abstract;
using BetterCommerce.Core.Extensions;
using BetterCommerce.Core.Utilities.Results;
using BetterCommerce.DataAccess.Abstract;
using BetterCommerce.Entity.Entities;

namespace BetterCommerce.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IBaseDal<Category> _categoryRepo;

        public CategoryManager(IBaseDal<Category> categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public IDataResult<IQueryable<Category>> GetMainCategoryList()
        {
            var mainCategories = _categoryRepo.GetAll()?.Where(x =>
                x.IsDeleted == false
                && x.ParentCategoryId == null);
            if (mainCategories == null) return new ErrorDataResult<IQueryable<Category>>("There is no category.");
            return new SuccessDataResult<IQueryable<Category>>(mainCategories);
        }

        public IDataResult<IQueryable<Category>> GetSubCategory(int categoryId)
        {
            var subCategories = _categoryRepo.GetBy(x =>
                x.ParentCategoryId == categoryId)?.Where(x => x.IsDeleted == false);
            if (subCategories == null) return new ErrorDataResult<IQueryable<Category>>("There is no sub category.");
            return new SuccessDataResult<IQueryable<Category>>(subCategories);
        }

        public IResult AddCategory(Category category)
        {
            throw new System.NotImplementedException();
        }

        public IResult EditCategory(Category category)
        {
            
        }

        public IResult DeleteCategory(Category category)
        {
            throw new System.NotImplementedException();
        }

        public IResult AddSubCategory(Category category)
        {
            throw new System.NotImplementedException();
        }

        public IResult EditSubCategory(Category category)
        {
            throw new System.NotImplementedException();
        }

        public IResult DeleteSubCategory(Category category)
        {
            throw new System.NotImplementedException();
        }
    }
}