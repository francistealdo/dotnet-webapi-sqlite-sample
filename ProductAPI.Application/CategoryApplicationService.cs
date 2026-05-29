using ProductAPI.Application.Dto;
using ProductAPI.Application.Interface;
using ProductAPI.Application.Interface.Mapper;
using ProductAPI.Domain.Core.Interface.Service;
using ProductAPI.Domain.Entity;

namespace ProductAPI.Application
{
    public class CategoryApplicationService : ICategoryApplicationService
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryMapper _categoryMapper;
        public CategoryApplicationService(ICategoryService categoryService, ICategoryMapper categoryMapper)
        {
            _categoryService = categoryService;
            _categoryMapper = categoryMapper;
        }

        public CategoryDto Add(CategoryDto categoryDto)
        {
            Category category = _categoryMapper.MapperDtoToEntity(categoryDto);
            Category result = _categoryService.Add(category);
            return _categoryMapper.MapperEntityToDto(result);
        }

        public void Delete(int id)
        {
            _categoryService.Delete(id);
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            IEnumerable<Category> categories = _categoryService.GetAll();
            return _categoryMapper.MapperListEntityToDto(categories);
        }

        public CategoryDto GetById(int id)
        {
            Category category = _categoryService.GetById(id);
            return _categoryMapper.MapperEntityToDto(category);
        }

        public CategoryDto Update(CategoryDto categoryDto)
        {
            Category category = _categoryMapper.MapperDtoToEntity(categoryDto);
            Category result = _categoryService.Update(category);
            return _categoryMapper.MapperEntityToDto(result);
        }
    }
}
