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

        public DbSet<Category> Categories
        {
            get; set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(p => p.Id).ValueGeneratedOnAdd();

                entity.HasIndex(p => p.Name)
                    .IsUnique();

                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id).ValueGeneratedOnAdd();

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(p => p.Description)
                    .HasMaxLength(250);

                entity.Property(p => p.SKU)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasIndex(p => p.SKU)
                    .IsUnique();

                entity.Property(p => p.Price)
                    .HasPrecision(10, 2)
                    .IsRequired();

                entity.Property(p => p.StockQuantity)
                    .IsRequired();

                entity.Property(p => p.IsActive)
                    .HasDefaultValue(true);

                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<Base>())
            {
                if (entry.State == EntityState.Added)
                    entry.Entity.CreatedDate = DateTime.UtcNow;

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedDate = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }
    }
}
