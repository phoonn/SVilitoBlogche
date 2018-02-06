using Interfaces.BusinessLogic;
using Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class BaseCrudLogic<T, F> : ICrudLogic<T> where T : class, new() where F : IRepository<T>
    {
        internal F Repo;
        internal IUnitOfWork Unit;

        public BaseCrudLogic(F Repo, IUnitOfWork Unit)
        {
            this.Repo = Repo;
            this.Unit = Unit;
        }

        public virtual void Add(T Item)
        {
            Repo.Save(Item);
            Unit.SaveChanges();
        }

        public virtual void Delete(T Item)
        {
            Repo.Delete(Item);
            Unit.SaveChanges();
        }

        public virtual void Edit(T Item)
        {
            Repo.Update(Item);
            Unit.SaveChanges();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Repo.Get();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int take = 0, int skip = 0)
        {
            return Repo.Get(filter, orderBy, includeProperties, take, skip);
        }

        public virtual T GetById(int id)
        {
            return Repo.GetById(id);
        }

        public T Find(Expression<Func<T, bool>> filter)
        {
            return Repo.Find(filter);
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Unit.Dispose();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
