using Microsoft.Extensions.DependencyInjection;
using Module01_Introduction_task_01.Context;
using Module01_Introduction_task_01.Entities;
using Module01_Introduction_task_01.Services;
using Module01_Introduction_task_01.Services.Interfaces;

namespace Module01_Introduction_task_01.Configuration
{
    public static class DIConfiguration
    {
        public static void RegisterServices(IServiceCollection config) {
            config.AddScoped<ICategoryService, CategoryService>();
            config.AddScoped<IProductService, ProductService>();
        }

        public static void RegisterRepository(IServiceCollection config) {
            config.AddScoped(typeof(IRepository<>), typeof(Repository<>);
        }
    }
}