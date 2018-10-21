using System;

namespace Northwind.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
