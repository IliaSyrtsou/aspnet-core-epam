using Northwind.Context;
using Northwind.Entities;
using Northwind.Services.Interfaces;

namespace Northwind.Services
{
    public class ProductService: BaseService<Product>, IProductService
    {
        public ProductService(IRepository<Product> repo, IUnitOfWork uow)
            : base(repo, uow) {}
    }
}