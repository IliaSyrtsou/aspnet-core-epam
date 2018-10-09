using System.Linq;
using Module01_Introduction_task_01.Context;
using Module01_Introduction_task_01.Entities;
using Module01_Introduction_task_01.Services.Interfaces;

namespace Module01_Introduction_task_01.Services {
    public class BaseService<T> : IBaseService<T> where T : BaseEntity {
        private IRepository<T> _repo { get; set; }
        private IUnitOfWork _uow { get; set; }
        public BaseService (IRepository<T> repo, IUnitOfWork uow) {
            this._repo = repo;
            this._uow = uow;
        }
        public IQueryable<T> GetAll () {
            return _repo.Query();
        }
    }
}