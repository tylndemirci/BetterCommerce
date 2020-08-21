using System;
using BetterCommerce.DataAccess.Abstract;

namespace BetterCommerce.DataAccess.Concrete
{
    public class UnifOfWork : IUnitOfWork
    {
        private readonly BetterCommerceContext _context;

        public UnifOfWork(BetterCommerceContext context)
        {
            _context = context;
        }

        public IBaseDal<T> GetRepository<T>() where T : class
        {
            return new EfBaseDal<T>(_context);
        }

        public int SaveChanges()
        {
            var i = _context.SaveChanges();
            return i;
        }
    }
}