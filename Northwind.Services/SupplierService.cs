using Northwind.Repository;
using Northwind.Entities;
using Northwind.Services.Interfaces;

namespace Northwind.Services
{
    public class SupplierService: BaseService<Supplier>, ISupplierService
    {
        public SupplierService(IRepository<Supplier> repo, IUnitOfWork uow)
            : base(repo, uow) {}
    }
}