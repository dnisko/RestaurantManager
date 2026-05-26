namespace Common.Exceptions.Supplier
{
    public class SupplierDataException : Exception
    {
        public SupplierDataException(string message) : base(message) { }
        public SupplierDataException(string message, Exception innerException) : base(message, innerException) { }
    }
}
