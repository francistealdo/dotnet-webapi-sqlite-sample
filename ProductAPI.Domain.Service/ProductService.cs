using ProductAPI.Domain.Core.Interface.Repository;
using ProductAPI.Domain.Core.Interface.Service;
using ProductAPI.Domain.Entity;

namespace ProductAPI.Domain.Service
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
