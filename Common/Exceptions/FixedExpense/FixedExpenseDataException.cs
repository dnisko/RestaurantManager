namespace Common.Exceptions.FixedExpense
{
    public class FixedExpenseDataException : Exception
    {
        public FixedExpenseDataException(string message) : base(message) { }
        public FixedExpenseDataException(string message, Exception innerException) : base(message, innerException) { }
    }
}
