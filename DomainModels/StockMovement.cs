namespace DomainModels
{
    public class StockMovement : BaseEntity
    {
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } = null!;

        public decimal Quantity { get; set; }
        public bool IsInbound { get; set; }
        public Enums.StockMovementType MovementType { get; set; }
        public string? Notes { get; set; }
    }
}
