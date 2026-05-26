namespace Common.Exceptions.Supplier
{
    public class SupplierNotFoundException : Exception
    {
        public SupplierNotFoundException(string message) : base(message)
        {
        }
        public SupplierNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
