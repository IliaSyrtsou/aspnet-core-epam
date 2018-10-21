using System.Linq;
using Northwind.Repository;
using Northwind.Entities;
using Northwind.Services.Interfaces;

namespace Northwind.Services {
    public class BaseService<T> : IBaseService<T> where T : BaseEntity {
        protected IRepository<T> _repo { get; set; }
        protected IUnitOfWork _uow { get; set; }
        public BaseService (IRepository<T> repo, IUnitOfWork uow) {
            this._repo = repo;
            this._uow = uow;
        }
        public IQueryable<T> GetAll () {
            return _repo.Query();
        }

        public void Add(T entity) {
            _repo.Add(entity);
        }

        public void Update(T entity) {
            _repo.Update(entity);
        }

        public void Remove(T entity) {
            _repo.Remove(entity);
        }

        public void SaveChanges() {
            _uow.Commit();
        }
    }
}