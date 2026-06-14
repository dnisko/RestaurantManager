namespace DTOs.COGS
{
    public class CogsLineDto
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public decimal CostPerUnit { get; set; }
        public decimal LineCost { get; set; } // Quantity × CostPerUnit
    }
}
