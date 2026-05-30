namespace ProductAPI.Application.Dto
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
            = new List<ProductDto>();
    }
}
