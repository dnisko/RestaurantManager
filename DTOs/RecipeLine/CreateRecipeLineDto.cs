namespace DTOs.RecipeLine
{
    public class CreateRecipeLineDto
    {
        public int ProductId { get; set; }
        public int IngredientId { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
    }
}
