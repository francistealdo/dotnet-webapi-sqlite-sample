using ProductAPI.Application.Dto;

namespace ProductAPI.Application.Interface
{
    public interface ICategoryApplicationService
    {
        CategoryDto Add(CategoryDto categoryDto);
        CategoryDto Update(CategoryDto categoryDto);
        void Delete(int id);
        IEnumerable<CategoryDto> GetAll();
        CategoryDto GetById(int id);
    }
}
