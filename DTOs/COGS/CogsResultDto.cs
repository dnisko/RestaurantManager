namespace DTOs.COGS
{
    public class CogsResultDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public List<CogsLineDto> Lines { get; set; } = new();
        public decimal TotalIngredientCost { get; set; }
        public decimal MarginPercent { get; set; }
        public decimal SuggestedPrice { get; set; }
        public decimal MarginAmount { get; set; } // SuggestedPrice - TotalIngredientCost
    }
}
