using System;

namespace BetterCommerce.DataAccess.Abstract
{
    public interface IUnitOfWork
    {
         IBaseDal<T> GetRepository<T>() where T : class;
         int SaveChanges();
    }
}