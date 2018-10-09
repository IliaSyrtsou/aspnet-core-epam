using Microsoft.EntityFrameworkCore;
using Northwind.Entities;

namespace Northwind.Context {
    public class NorthwindDbContext : DbContext {
        public NorthwindDbContext(DbContextOptions options): base(options) {}
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}