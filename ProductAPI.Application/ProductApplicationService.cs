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

        public void Add(ProductDto productDto)
        {
            Product product = _mapper.MapperDtoToEntity(productDto);
            _service.Add(product);
        }

        public void Delete(ProductDto productDto)
        {
            Product product = _service.GetById(productDto.Id);
            _service.Delete(product);
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
