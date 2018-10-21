using Northwind.Repository;
using Northwind.Entities;
using Northwind.Services.Interfaces;

namespace Northwind.Services
{
    public class CategoryService: BaseService<Category>, ICategoryService
    {
        public CategoryService(IRepository<Category> repo, IUnitOfWork uow)
            : base(repo, uow) {}
    }
}