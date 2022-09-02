using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVC_UnitOfWork_Repository
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(int entityId);
        void Delete(T entity);
        IQueryable<T> Search(Expression<Func<T, bool>> expression);
        T FirstOrDefault();
        T FirstOrDefault(Expression<Func<T, bool>> expression);
        bool Any(Expression<Func<T, bool>> expression);
        IQueryable<T> ToList();
        T Get(int id);
        IQueryable<T> Take(int count);
        int Count();
        int Count(Expression<Func<T, bool>> expression);
    }
}
