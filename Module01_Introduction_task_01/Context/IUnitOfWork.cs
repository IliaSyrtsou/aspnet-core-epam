using System;

namespace Module01_Introduction_task_01.Context
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
