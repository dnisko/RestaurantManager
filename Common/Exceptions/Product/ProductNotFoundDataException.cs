namespace Common.Exceptions.Product
{
    public class ProductNotFoundDataException : Exception
    {
        public ProductNotFoundDataException(string message) : base(message)
        {
        }
        public ProductNotFoundDataException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
