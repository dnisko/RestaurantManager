namespace DTOs.Product
{
    public class CreateProductDto
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
