using System;

namespace Northwind.Context
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
