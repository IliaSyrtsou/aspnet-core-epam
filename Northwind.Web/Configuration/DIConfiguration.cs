using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Northwind.Repository;
using Northwind.Services;
using Northwind.Services.Interfaces;

namespace Northwind.Web.Configuration
{
    public static class DIConfiguration
    {
        public static void RegisterServices(IServiceCollection config) {
            config.AddScoped<ICategoryService, CategoryService>();
            config.AddScoped<ISupplierService, SupplierService>();
            config.AddScoped<IProductService, ProductService>();
        }

        public static void RegisterRepository(IServiceCollection config) {
            config.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            config.AddScoped<IUnitOfWork, EFUnitOfWork>();
            config.AddScoped<DbContext, NorthwindDbContext>();
        }

        public static void RegisterFilters(IServiceCollection config) {
            config.AddScoped<LoggingActionFilter>();
        }
    }
}