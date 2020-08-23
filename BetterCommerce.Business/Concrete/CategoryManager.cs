using System;
using System.Collections.Generic;
using System.Linq;
using BetterCommerce.Business.Abstract;
using BetterCommerce.Core.Extensions;
using BetterCommerce.Core.Utilities.Results;
using BetterCommerce.DataAccess.Abstract;
using BetterCommerce.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace BetterCommerce.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IBaseDal<Category> _categoryRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryManager(IBaseDal<Category> categoryRepo, IUnitOfWork unitOfWork)
        {
            _categoryRepo = categoryRepo;
            _unitOfWork = unitOfWork;
        }

        public IDataResult<IQueryable<Category>> GetMainCategoryList()
        {
            var mainCategories = _categoryRepo.GetAll()?.Where(x =>
                x.IsDeleted == false
                && x.ParentCategoryId == null)
                .Include(x=>x.SubCategories)
                .Include(x=>x.ParentCategory);
            return mainCategories != null 
                ? (IDataResult<IQueryable<Category>>) new SuccessDataResult<IQueryable<Category>>(mainCategories) 
                : new ErrorDataResult<IQueryable<Category>>("There is no category.");
        }

        public IDataResult<IQueryable<Category>> GetSubCategory(int categoryId)
        {
            var subCategories = _categoryRepo.GetBy(x =>
                x.ParentCategoryId == categoryId)?.Where(x => x.IsDeleted == false)
                .Include(x=>x.SubCategories)
                .Include(x=>x.ParentCategory);
            return subCategories != null
                ? (IDataResult<IQueryable<Category>>) new SuccessDataResult<IQueryable<Category>>(subCategories)
                : new ErrorDataResult<IQueryable<Category>>("There is no sub category.");
        }

        public IResult AddCategory(Category category)
        {
            if (category == null) return new ErrorResult("Category is empty.");
            if (category.Name.IsNullS()) return new ErrorResult("Category name is empty.");
            var newCategory = new Category();
            newCategory.Name = category.Name;
            if (category.ParentCategoryId != null) newCategory.ParentCategoryId = category.ParentCategoryId;
            newCategory.CreatedAt = DateTime.Now;
            _categoryRepo.Create(newCategory);
            var result = _unitOfWork.SaveChanges();
            return result > 0
                ? (IResult) new SuccessResult("Category successfully added.")
                : new ErrorResult("Category not added.");
        }

        public IResult EditCategory(Category category)
        {
            if (category.Name.IsNullS()) return new ErrorResult("Category name is empty.");
            var editingCategory = _categoryRepo.GetBy(x => x.Id == category.Id)?.FirstOrDefault();
            if (editingCategory==null) return new ErrorResult("Category not found");
            editingCategory.Name = category.Name;
            if (category.ParentCategoryId != null) editingCategory.ParentCategoryId = category.ParentCategoryId;
            editingCategory.CreatedAt = category.CreatedAt;
            editingCategory.ModifiedAt = DateTime.Now;
            _categoryRepo.Update(editingCategory);
            var result = _unitOfWork.SaveChanges();
            return result > 0
                ? (IResult) new SuccessResult("Category successfully updated.")
                : new ErrorResult("Category not updated.");
        }

        public IResult DeleteCategory(Category category)
        {
            var deletingCategory = _categoryRepo.GetBy(x => x.Id == category.Id)?.FirstOrDefault();
            if (deletingCategory==null) return new ErrorResult("Category not found");
            var checkForSubs = _categoryRepo.GetBy(x => x.ParentCategoryId == deletingCategory.Id);
            deletingCategory.IsDeleted = true;
            deletingCategory.ModifiedAt= DateTime.Now;
            _categoryRepo.Update(deletingCategory);
            if (checkForSubs!=null)
            {
                var subs = new List<Category>();
                subs.AddRange(checkForSubs);
                for (int i = 0; i <= subs.Count; i++)
                {
                    subs[i].IsDeleted = true;
                    _categoryRepo.Update(subs[i]);
                    var checkAgain = _categoryRepo.GetBy(x => x.ParentCategoryId == subs[i].Id);
                    subs.AddRange(checkAgain);
                }
            }
            _categoryRepo.Update(deletingCategory);
            var result = _unitOfWork.SaveChanges();
            return result > 0
                ? (IResult) new SuccessResult("Category successfully deleted.")
                : new ErrorResult("Category not deleted.");
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