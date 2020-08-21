using System;
using System.Linq;
using System.Linq.Expressions;
using BetterCommerce.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BetterCommerce.DataAccess.Concrete
{
    public class EfBaseDal<T> : IBaseDal<T> where T : class
    {
        private readonly BetterCommerceContext _context;


        public EfBaseDal(BetterCommerceContext context)
        {
            _context = context;

        }
        
        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public IQueryable<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public void Create(T entity)
        {
            var adding = _context.Entry(entity);
            adding.State = EntityState.Added;
        }

        public void Update(T entity)
        {
            var updating = _context.Entry(entity);
            updating.State = EntityState.Modified;
        }

       
    }
}