namespace Common.Exceptions.Category
{
    public class CategoryDataException : Exception
    {
        public CategoryDataException(string message) : base(message) { }
        public CategoryDataException(string message, Exception innerException) : base(message, innerException) { }
    }
}
