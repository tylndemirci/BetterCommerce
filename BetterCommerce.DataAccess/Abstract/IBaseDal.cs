using System;
using System.Linq;
using System.Linq.Expressions;

namespace BetterCommerce.DataAccess.Abstract
{
    public interface IBaseDal<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);
        void Create(T entity);
        void Update(T entity);
    }
}