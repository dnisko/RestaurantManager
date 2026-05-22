namespace DomainModels
{
    public class IngredientPrice : BaseEntity
    {
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } = null!;

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;

        public decimal Price { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}
