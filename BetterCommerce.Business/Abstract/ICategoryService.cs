using System.Linq;
using BetterCommerce.Core.Utilities.Results;
using BetterCommerce.Entity.Entities;

namespace BetterCommerce.Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<IQueryable<Category>> GetMainCategoryList();
        IDataResult<IQueryable<Category>> GetSubCategory(int categoryId);
        IResult AddCategory(Category category);
        IResult EditCategory(Category category);
        IResult DeleteCategory(Category category);
        IResult AddSubCategory(Category category);
    }
}    