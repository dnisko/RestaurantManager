namespace Common.Exceptions.RecipeLine
{
    public class RecipeLineNotFoundException : Exception
    {
        public RecipeLineNotFoundException(string message) : base(message) { }
        public RecipeLineNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
