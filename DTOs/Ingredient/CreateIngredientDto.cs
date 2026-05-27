namespace DTOs.Ingredient
{
    public class CreateIngredientDto
    {
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public decimal MinimumQuantity { get; set; }
    }
}
