using Microsoft.EntityFrameworkCore;
using Module01_Introduction_task_01.Entities;

namespace Module01_Introduction_task_01.Context {
    public class ApplicationDbContext : DbContext {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}