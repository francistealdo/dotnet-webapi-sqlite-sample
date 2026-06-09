using FluentAssertions;
using Moq;
using ProductAPI.Application;
using ProductAPI.Application.Dto;
using ProductAPI.Application.Interface.Mapper;
using ProductAPI.Domain.Core.Interface.Service;
using ProductAPI.Domain.Entity;

namespace ProductAPI.UnitTest
{
    public class ProductApplicationServiceTests
    {
        private readonly Mock<IProductService> _productServiceMock;
        private readonly Mock<IProductMapper> _productMapperMock;
        private readonly ProductApplicationService _service;

        public ProductApplicationServiceTests()
        {
            _productServiceMock = new Mock<IProductService>();
            _productMapperMock = new Mock<IProductMapper>();

            _service = new ProductApplicationService(
                _productServiceMock.Object,
                _productMapperMock.Object);
        }

        [Fact]
        public void GetById_ShouldReturnProductDto_WhenProductExists()
        {
            // Arrange

            var product = new Product
            {
                Id = 1,
                Name = "Notebook"
            };

            var productDto = new ProductDto
            {
                Id = 1,
                Name = "Notebook"
            };

            var productServiceMock =
                new Mock<IProductService>();

            var mapperMock =
                new Mock<IProductMapper>();

            productServiceMock
                .Setup(x => x.GetById(1))
                .Returns(product);

            mapperMock
                .Setup(x => x.MapperEntityToDto(product))
                .Returns(productDto);

            var applicationService =
                new ProductApplicationService(
                    productServiceMock.Object,
                    mapperMock.Object);

            // Act

            var result = applicationService.GetById(1);

            // Assert

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Name.Should().Be("Notebook");
        }

        [Fact]
        public void GetById_ShouldReturnNull_WhenProductDoesNotExist()
        {
            _productServiceMock
                .Setup(x => x.GetById(99))
                .Returns((Product?)null);

            var result = _service.GetById(99);

            result.Should().BeNull();
        }

        [Fact]
        public void GetAll_ShouldReturnProductDtoCollection()
        {
            var products = new List<Product>
        {
            new Product { Id = 1, Name = "Notebook" },
            new Product { Id = 2, Name = "Mouse" }
        };

            var productDtos = new List<ProductDto>
        {
            new ProductDto { Id = 1, Name = "Notebook" },
            new ProductDto { Id = 2, Name = "Mouse" }
        };

            _productServiceMock
                .Setup(x => x.GetAll())
                .Returns(products);

            _productMapperMock
                .Setup(x => x.MapperListEntityToDto(products))
                .Returns(productDtos);

            var result = _service.GetAll();

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.First().Name.Should().Be("Notebook");
        }

        [Fact]
        public void Add_ShouldReturnCreatedProductDto()
        {
            var productDto = new ProductDto
            {
                Name = "Keyboard",
                SKU = "KEY-001",
                Price = 150,
                StockQuantity = 10
            };

            var product = new Product
            {
                Name = "Keyboard",
                SKU = "KEY-001",
                Price = 150,
                StockQuantity = 10
            };

            var createdProduct = new Product
            {
                Id = 1,
                Name = "Keyboard",
                SKU = "KEY-001",
                Price = 150,
                StockQuantity = 10
            };

            var createdProductDto = new ProductDto
            {
                Id = 1,
                Name = "Keyboard",
                SKU = "KEY-001",
                Price = 150,
                StockQuantity = 10
            };

            _productMapperMock
                .Setup(x => x.MapperDtoToEntity(productDto))
                .Returns(product);

            _productServiceMock
                .Setup(x => x.Add(product))
                .Returns(createdProduct);

            _productMapperMock
                .Setup(x => x.MapperEntityToDto(createdProduct))
                .Returns(createdProductDto);

            var result = _service.Add(productDto);

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Name.Should().Be("Keyboard");

            _productServiceMock.Verify(x => x.Add(product), Times.Once);
        }

        [Fact]
        public void Update_ShouldReturnUpdatedProductDto()
        {
            var productDto = new ProductDto
            {
                Id = 1,
                Name = "Updated Notebook",
                SKU = "NOTE-001",
                Price = 4000,
                StockQuantity = 5
            };

            var product = new Product
            {
                Id = 1,
                Name = "Updated Notebook",
                SKU = "NOTE-001",
                Price = 4000,
                StockQuantity = 5
            };

            _productMapperMock
                .Setup(x => x.MapperDtoToEntity(productDto))
                .Returns(product);

            _productServiceMock
                .Setup(x => x.Update(product))
                .Returns(product);

            _productMapperMock
                .Setup(x => x.MapperEntityToDto(product))
                .Returns(productDto);

            var result = _service.Update(productDto);

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Name.Should().Be("Updated Notebook");

            _productServiceMock.Verify(x => x.Update(product), Times.Once);
        }

        [Fact]
        public void Delete_ShouldCallProductServiceDelete()
        {
            var productId = 1;

            _service.Delete(productId);

            _productServiceMock.Verify(
                x => x.Delete(productId),
                Times.Once);
        }
    }
}
