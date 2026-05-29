using ProductAPI.Domain.Core.Interface.Repository;
using ProductAPI.Domain.Entity;

namespace ProductAPI.Infrastructure.Data.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
