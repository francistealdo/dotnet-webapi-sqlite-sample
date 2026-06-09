using FluentAssertions;
using Moq;
using ProductAPI.Application;
using ProductAPI.Application.Dto;
using ProductAPI.Application.Interface.Mapper;
using ProductAPI.Domain.Core.Interface.Service;
using ProductAPI.Domain.Entity;

namespace ProductAPI.UnitTest.Application;

public class CategoryApplicationServiceTests
{
    private readonly Mock<ICategoryService> _categoryServiceMock;
    private readonly Mock<ICategoryMapper> _categoryMapperMock;
    private readonly CategoryApplicationService _service;

    public CategoryApplicationServiceTests()
    {
        _categoryServiceMock = new Mock<ICategoryService>();
        _categoryMapperMock = new Mock<ICategoryMapper>();

        _service = new CategoryApplicationService(
            _categoryServiceMock.Object,
            _categoryMapperMock.Object);
    }

    [Fact]
    public void GetById_ShouldReturnCategoryDto_WhenCategoryExists()
    {
        var category = new Category { Id = 1, Name = "Electronics" };
        var categoryDto = new CategoryDto { Id = 1, Name = "Electronics" };

        _categoryServiceMock.Setup(x => x.GetById(1))
            .Returns(category);

        _categoryMapperMock.Setup(x => x.MapperEntityToDto(category))
            .Returns(categoryDto);

        var result = _service.GetById(1);

        result.Should().NotBeNull();
        result!.Id.Should().Be(1);
        result.Name.Should().Be("Electronics");
    }

    [Fact]
    public void GetById_ShouldReturnNull_WhenCategoryDoesNotExist()
    {
        _categoryServiceMock.Setup(x => x.GetById(99))
            .Returns((Category?)null);

        var result = _service.GetById(99);

        result.Should().BeNull();
    }

    [Fact]
    public void GetAll_ShouldReturnCategoryDtoCollection()
    {
        var categories = new List<Category>
        {
            new Category { Id = 1, Name = "Electronics" },
            new Category { Id = 2, Name = "Books" }
        };

        var categoryDtos = new List<CategoryDto>
        {
            new CategoryDto { Id = 1, Name = "Electronics" },
            new CategoryDto { Id = 2, Name = "Books" }
        };

        _categoryServiceMock.Setup(x => x.GetAll())
            .Returns(categories);

        _categoryMapperMock.Setup(x => x.MapperListEntityToDto(categories))
            .Returns(categoryDtos);

        var result = _service.GetAll();

        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.First().Name.Should().Be("Electronics");
    }

    [Fact]
    public void Add_ShouldReturnCreatedCategoryDto()
    {
        var categoryDto = new CategoryDto { Name = "Games" };
        var category = new Category { Name = "Games" };
        var createdCategory = new Category { Id = 1, Name = "Games" };
        var createdCategoryDto = new CategoryDto { Id = 1, Name = "Games" };

        _categoryMapperMock.Setup(x => x.MapperDtoToEntity(categoryDto))
            .Returns(category);

        _categoryServiceMock.Setup(x => x.Add(category))
            .Returns(createdCategory);

        _categoryMapperMock.Setup(x => x.MapperEntityToDto(createdCategory))
            .Returns(createdCategoryDto);

        var result = _service.Add(categoryDto);

        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Name.Should().Be("Games");

        _categoryServiceMock.Verify(x => x.Add(category), Times.Once);
    }

    [Fact]
    public void Update_ShouldReturnUpdatedCategoryDto()
    {
        var categoryDto = new CategoryDto { Id = 1, Name = "Updated Category" };
        var category = new Category { Id = 1, Name = "Updated Category" };

        _categoryMapperMock.Setup(x => x.MapperDtoToEntity(categoryDto))
            .Returns(category);

        _categoryServiceMock.Setup(x => x.Update(category))
            .Returns(category);

        _categoryMapperMock.Setup(x => x.MapperEntityToDto(category))
            .Returns(categoryDto);

        var result = _service.Update(categoryDto);

        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Name.Should().Be("Updated Category");

        _categoryServiceMock.Verify(x => x.Update(category), Times.Once);
    }

    [Fact]
    public void Delete_ShouldCallCategoryServiceDelete()
    {
        var categoryId = 1;

        _service.Delete(categoryId);

        _categoryServiceMock.Verify(
            x => x.Delete(categoryId),
            Times.Once);
    }
}