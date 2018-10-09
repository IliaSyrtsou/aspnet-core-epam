using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Northwind.Context;
using Northwind.Entities;
using Northwind.Services;
using Northwind.Services.Interfaces;

namespace Northwind.Configuration
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
    }
}