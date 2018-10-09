using Module01_Introduction_task_01.Context;
using Module01_Introduction_task_01.Entities;
using Module01_Introduction_task_01.Services.Interfaces;

namespace Module01_Introduction_task_01.Services
{
    public class ProductService: BaseService<Product>, IProductService
    {
        public ProductService(IRepository<Product> repo, IUnitOfWork uow)
            : base(repo, uow) {}
    }
}