using Microsoft.EntityFrameworkCore;
using ProductAPI.Domain.Core.Interface.Repository;
using ProductAPI.Domain.Entity;

namespace ProductAPI.Infrastructure.Data.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override IEnumerable<Category> GetAll()
        {
            return _context.Categories
                .Include(c => c.Products)
                .ToList();
        }
    }
}
