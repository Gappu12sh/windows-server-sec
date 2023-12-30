using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    public class DataRepository<T> : IDataRepository<T> where T : class
    {
        protected CustomerApplicationContext _context;
        private readonly DbSet<T> _dbSet;
        public DataRepository(CustomerApplicationContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void SoftDelete(T entity)
        {
            var prop = typeof(T).GetProperty("IsActive");
            if (prop != null)
            {
                prop.SetValue(entity, false);
            }
            else
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
                _dbSet.Remove(entity);
            }
        }

        public void DeleteWhere<TEntity>(Expression<Func<TEntity, bool>> predicate = null) where TEntity : class
        {
            var dbSet=_context.Set<TEntity>();
            if(predicate != null)
            {
                dbSet.RemoveRange(dbSet.Where(predicate));
            }
            else
            {
                dbSet.RemoveRange(dbSet);
                _context.SaveChanges();
            }
        }

        public IQueryable<T> FindAll()
        {
            return _dbSet ?? new List<T>() as IQueryable<T>;
        }

        public T Get(object id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(T entity)
        {
            ActiveDefault(entity);
            _dbSet.Add(entity);
        }

        private void ActiveDefault(T entity)
        {
            var prop = typeof(T).GetProperty("IsActive");
            if (prop != null)
            {
                prop.SetValue(entity, true);
            }
        }

        public void HardDelete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }
    }
}
