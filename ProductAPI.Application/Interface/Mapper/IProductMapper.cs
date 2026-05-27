using ProductAPI.Application.Dto;
using ProductAPI.Domain.Entity;

namespace ProductAPI.Application.Interface.Mapper
{
    public interface IProductMapper
    {
        ProductDto MapperEntityToDto(Product product);
        IEnumerable<ProductDto> MapperListEntityToDto(IEnumerable<Product> product);
        Product MapperDtoToEntity(ProductDto dto);
    }
}
