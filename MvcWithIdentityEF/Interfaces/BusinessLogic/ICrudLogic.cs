using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.BusinessLogic
{
    public interface ICrudLogic<T> : IDisposable
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", int take = 0, int skip = 0);

        void Add(T Item);

        void Edit(T Item);

        void Delete(T Item);

        T GetById(int id);

        T Find(Expression<Func<T, bool>> filter);
    }
}
