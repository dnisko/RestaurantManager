namespace DomainModels
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }
        //public string Unit { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
