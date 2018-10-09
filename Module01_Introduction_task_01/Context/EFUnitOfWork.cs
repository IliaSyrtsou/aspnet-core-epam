using Microsoft.EntityFrameworkCore;

namespace Module01_Introduction_task_01.Context
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;

        public EFUnitOfWork(DbContext context)
        {
            this.context = context;
        }

        internal DbSet<T> GetDbSet<T>()
            where T : class
        {
            return context.Set<T>();
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}