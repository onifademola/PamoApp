using System;
using System.Linq;
using System.Linq.Expressions;

namespace Repo.Repos
{
    public interface IRepositoryBase<T> : IDisposable where T : class
    {
        void Edit(T entity);
        T Find(string cid);
        T Find(int cid);
        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Insert(T entity);
        void Delete(T entity);
        void Delete(int id);
        void Save();
    }
}
