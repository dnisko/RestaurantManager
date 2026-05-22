namespace DomainModels
{
    public class RecipeLine : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } = null!;

        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
    }
}
