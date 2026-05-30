using Microsoft.EntityFrameworkCore;
using ProductAPI.Domain.Core.Interface.Repository;
using ProductAPI.Domain.Entity;

namespace ProductAPI.Infrastructure.Data.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override IEnumerable<Product> GetAll()
        {
            return _context.Products
                .Include(p => p.Category)
                .ToList();
        }

        public override Product GetById(int id)
        {
            return _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);
        }
    }
}
