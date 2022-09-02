using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVC_UnitOfWork_Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext _dbContext;
        protected DbSet<T> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Any(expression);
        }

        public int Count()
        {
            return _dbSet.Count();
        }

        public int Count(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Count(expression);
        }

        public void Delete(int entityId)
        {
            T deletedItem = _dbSet.Find(entityId);
            Delete(deletedItem);
        }

        public void Delete(T entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public T FirstOrDefault()
        {
            return _dbSet.FirstOrDefault();
        }

        public T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).FirstOrDefault();
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<T> Search(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public IQueryable<T> Take(int count)
        {
            return _dbSet.Take(count);
        }

        public IQueryable<T> ToList()
        {
            return _dbSet;
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
