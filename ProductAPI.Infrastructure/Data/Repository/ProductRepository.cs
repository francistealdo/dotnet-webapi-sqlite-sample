using ProductAPI.Domain.Core.Interface.Repository;
using ProductAPI.Domain.Entity;

namespace ProductAPI.Infrastructure.Data.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
