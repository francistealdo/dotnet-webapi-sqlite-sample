using ProductAPI.Application.Dto;

namespace ProductAPI.Application.Interface
{
    public interface IProductApplicationService
    {
        ProductDto Add(ProductDto productDto);
        ProductDto Update(ProductDto productDto);
        void Delete(int id);
        IEnumerable<ProductDto> GetAll();
        ProductDto GetById(int id);
    }
}
