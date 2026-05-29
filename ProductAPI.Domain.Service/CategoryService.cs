using ProductAPI.Domain.Core.Interface.Repository;
using ProductAPI.Domain.Core.Interface.Service;

namespace ProductAPI.Domain.Service
{
    public class CategoryService : BaseService<Entity.Category>, ICategoryService
    {
        private readonly ICategoryRepository _repository;
        public CategoryService(ICategoryRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
