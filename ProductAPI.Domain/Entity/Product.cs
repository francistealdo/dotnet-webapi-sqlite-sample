namespace ProductAPI.Domain.Entity
{
    public class Product : Base
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public bool IsActive { get; set; }
    }
}
