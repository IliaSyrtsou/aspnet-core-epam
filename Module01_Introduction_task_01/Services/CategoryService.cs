using Module01_Introduction_task_01.Context;
using Module01_Introduction_task_01.Entities;
using Module01_Introduction_task_01.Services.Interfaces;

namespace Module01_Introduction_task_01.Services
{
    public class CategoryService: BaseService<Category>, ICategoryService
    {
        public CategoryService(IRepository<Category> repo, IUnitOfWork uow)
            : base(repo, uow) {}
    }
}