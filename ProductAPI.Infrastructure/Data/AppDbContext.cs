using Microsoft.EntityFrameworkCore;
using ProductAPI.Domain.Entity;

namespace ProductAPI.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products
        {
            get; set;
        }
    }
}
