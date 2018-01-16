using System;
using System.Data.Entity;

namespace Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }

        void SaveChanges();
    }
}
