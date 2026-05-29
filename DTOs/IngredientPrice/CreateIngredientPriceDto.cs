namespace DTOs.IngredientPrice
{
    public class CreateIngredientPriceDto
    {
        public int IngredientId { get; set; }
        public int SupplierId { get; set; }
        public decimal Price { get; set; }
        public DateTime ValidFrom { get; set; } = DateTime.UtcNow.Date;
        public DateTime? ValidTo { get; set; }
        public bool IsPreferred { get; set; }
    }
}
