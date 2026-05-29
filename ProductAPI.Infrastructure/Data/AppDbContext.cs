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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id).ValueGeneratedOnAdd();

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(p => p.Description)
                    .HasMaxLength(250);
            });

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<Base>())
            {
                if (entry.State == EntityState.Added)
                    entry.Entity.CreatedDate = DateTime.UtcNow;
            }

            return base.SaveChanges();
        }
    }
}
