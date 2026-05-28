namespace Common.Exceptions.Product
{
    public class ProductDataException : Exception
    {
        public ProductDataException(string message) : base(message) { }
        public ProductDataException(string message, Exception innerException) : base(message, innerException) { }
    }
}
