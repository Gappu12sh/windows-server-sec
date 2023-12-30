using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    public interface IDataRepository<T>
    {
        IQueryable<T> FindAll();

        T Get(object id);

        void Insert(T entity);

        void SoftDelete(T entity);
        void HardDelete(T entity);


        void DeleteWhere<TEntity>(Expression<Func<TEntity, bool>> predicate = null) where TEntity : class;
    }
}
