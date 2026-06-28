namespace DTOs.FixedExpense
{
    public class FixedExpenseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Amount { get; set; } // e.g. 34.15
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}
