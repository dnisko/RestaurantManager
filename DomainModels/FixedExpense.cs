using DomainModels.Enums;

namespace DomainModels
{
    public class FixedExpense : BaseEntity
    {
        public string Name { get; set; } = null!;
        public decimal Amount { get; set; } // e.g. 34.15
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public FixedExpenseCategory Category { get; set; }
    }
}
