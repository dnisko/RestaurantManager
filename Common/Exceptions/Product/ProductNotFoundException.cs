namespace Common.Exceptions.Product
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string message) : base(message)
        {
        }
        public ProductNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
