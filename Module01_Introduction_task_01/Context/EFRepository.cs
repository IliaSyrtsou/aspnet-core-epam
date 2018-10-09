using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Module01_Introduction_task_01.Entities;

namespace Module01_Introduction_task_01.Context {
    public class EFRepository<T> : IRepository<T> where T : class {
        private readonly DbSet<T> _dbSet;

        public EFRepository(IUnitOfWork unitOfWork)
        {
            var efUnitOfWork = unitOfWork as EFUnitOfWork;
            if (efUnitOfWork == null) throw new Exception("Expected EFUnitOfWork");
            _dbSet = efUnitOfWork.GetDbSet<T>();
        }

        public void Add(T item)
        {
            _dbSet.Add(item);
        }

        public void Remove(T item)
        {
            _dbSet.Remove(item);
        }

        public IQueryable<T> Query()
        {
            return _dbSet;
        }
    }
}