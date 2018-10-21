using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Repository {
    public class EFRepository<T> : IRepository<T> where T : class {
        private readonly DbSet<T> _dbSet;

        public EFRepository (IUnitOfWork unitOfWork) {
            var efUnitOfWork = unitOfWork as EFUnitOfWork;
            if (efUnitOfWork == null) throw new Exception ("Expected EFUnitOfWork");
            _dbSet = efUnitOfWork.GetDbSet<T> ();
        }

        public void Add (T item) {
            _dbSet.Add(item);
        }

        public void Remove (T item) {
            _dbSet.Remove(item);
        }

        public void Update (T item) {
            _dbSet.Attach(item).State = EntityState.Modified;
        }

        public IQueryable<T> Query () {
            return _dbSet.AsNoTracking();
        }
    }
}