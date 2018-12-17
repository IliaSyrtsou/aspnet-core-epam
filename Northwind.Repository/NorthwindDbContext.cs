using Microsoft.EntityFrameworkCore;
using Northwind.Entities;

namespace Northwind.Repository {
    public class NorthwindDbContext : DbContext {
        public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options): base(options) {}
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}