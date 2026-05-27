namespace DTOs.Ingredient
{
    public class UpdateIngredientDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public decimal QuantityOnHand { get; set; }
        public decimal MinimumQuantity { get; set; }
    }
}
