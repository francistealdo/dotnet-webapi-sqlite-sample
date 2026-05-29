using ProductAPI.Application.Dto;
using ProductAPI.Application.Interface;
using ProductAPI.Application.Interface.Mapper;
using ProductAPI.Domain.Core.Interface.Service;
using ProductAPI.Domain.Entity;

namespace ProductAPI.Application
{
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly IProductService _service;
        private readonly IProductMapper _mapper;

        public ProductApplicationService(IProductService service, IProductMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public ProductDto Add(ProductDto productDto)
        {
            Product product = _mapper.MapperDtoToEntity(productDto);
            Product result = _service.Add(product);
            return _mapper.MapperEntityToDto(result);
        }

        public ProductDto Update(ProductDto productDto)
        {
            Product product = _mapper.MapperDtoToEntity(productDto);
            Product result = _service.Update(product);
            return _mapper.MapperEntityToDto(result);
        }

        public void Delete(int id)
        {
            _service.Delete(id);
        }

        public IEnumerable<ProductDto> GetAll()
        {
            IEnumerable<Product> products = _service.GetAll();
            return _mapper.MapperListEntityToDto(products);
        } 

        public ProductDto GetById(int id)
        {
            Product product = _service.GetById(id);
            return _mapper.MapperEntityToDto(product);
        }
    }
}
