using ProductAPI.Application.Dto;
using ProductAPI.Application.Interface.Mapper;
using ProductAPI.Domain.Entity;

namespace ProductAPI.Application.Mapper
{
    public class ProductMapper : IProductMapper
    {
        public ProductDto MapperEntityToDto(Product product)
        {
            ProductDto? dto = product != null ? new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description
            } : null;

            return dto;
        }

        public IEnumerable<ProductDto> MapperListEntityToDto(IEnumerable<Product> product)
            => product.Select(p => MapperEntityToDto(p));

        public Product MapperDtoToEntity(ProductDto dto)
        {
            Product product = new Product()
            {
                Name = dto.Name,
                Description = dto.Description
            };

            return product;
        }
    }
}
