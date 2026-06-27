namespace Common.Exceptions.FixedExpense
{
    public class FixedExpenseNotFoundException : Exception
    {
        public FixedExpenseNotFoundException(string message) : base(message) { }
        public FixedExpenseNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
