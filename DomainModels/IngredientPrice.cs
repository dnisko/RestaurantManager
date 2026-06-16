namespace DomainModels
{
    public class IngredientPrice : BaseEntity
    {
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } = null!;

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;

        public decimal Price { get; set; }      // e.g. 34.15
        //public string PriceUnit { get; set; }  // e.g. "kg"
        public int PriceQuantity { get; set; } // e.g. 1 (1kg for 34.15€)
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public bool IsPreferred { get; set; }
    }
}
