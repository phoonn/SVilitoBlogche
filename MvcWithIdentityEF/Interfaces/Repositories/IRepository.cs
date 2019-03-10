using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Interfaces.Repositories
{
    public interface IRepository<T> :IDisposable where T : class, new()
    {
        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", int take = 0, int skip = 0);

        List<T> GetAll();

        T GetById(int id);

        T Find(Expression<Func<T, bool>> filter);

        void Save(T entity);


        void Delete(object id);


        void Delete(T entityToDelete);


        void Update(T entityToUpdate);

        void Commit();
    }
}
