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
                Description = product.Description,
                SKU = product.SKU,
                CategoryId = product.CategoryId,
                CategoryName = product.Category != null ? product.Category.Name : null,
                IsActive = product.IsActive,
                Price = product.Price,
                StockQuantity = product.StockQuantity
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
                Description = dto.Description,
                SKU = dto.SKU,
                CategoryId= dto.CategoryId,
                IsActive= dto.IsActive,
                Price= dto.Price,
                StockQuantity= dto.StockQuantity
            };

            return product;
        }
    }
}
