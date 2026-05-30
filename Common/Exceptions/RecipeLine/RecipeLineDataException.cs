namespace Common.Exceptions.RecipeLine
{
    public class RecipeLineDataException : Exception
    {
        public RecipeLineDataException(string message) : base(message) { }
        public RecipeLineDataException(string message, Exception innerException) : base(message, innerException) { }
    }
}
