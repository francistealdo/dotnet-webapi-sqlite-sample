using ProductAPI.Application.Dto;
using ProductAPI.Application.Interface.Mapper;
using ProductAPI.Domain.Entity;

namespace ProductAPI.Application.Mapper
{
    public class CategoryMapper : ICategoryMapper
    {
        public readonly IProductMapper _productMapper;
        public CategoryMapper(IProductMapper productMapper)
        {
            _productMapper = productMapper;
        }

        public Category MapperDtoToEntity(CategoryDto dto)
        {
            Category category = new Category()
            {
                Name = dto.Name,
            };

            return category;
        }

        public CategoryDto MapperEntityToDto(Category entity)
        {
            CategoryDto? dto = entity != null ? new CategoryDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Products = _productMapper.MapperListEntityToDto(entity.Products)
            } : null;

            return dto;
        }

        public IEnumerable<CategoryDto> MapperListEntityToDto(IEnumerable<Category> entities)
            => entities.Select(e => MapperEntityToDto(e));
    }
}
