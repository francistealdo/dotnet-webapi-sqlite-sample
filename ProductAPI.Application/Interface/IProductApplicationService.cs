using ProductAPI.Application.Dto;

namespace ProductAPI.Application.Interface
{
    public interface IProductApplicationService
    {
        void Add(ProductDto productDto);
        void Delete(ProductDto productDto);
        IEnumerable<ProductDto> GetAll();
        ProductDto GetById(int id);
    }
}
