namespace ProductAPI.Domain.Entity
{
    public class Category : Base
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
            = new List<Product>();
    }
}
