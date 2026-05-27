namespace Common.Exceptions.Ingredient
{
    public class IngredientDataException : Exception
    {
        public IngredientDataException(string message) : base(message) { }
        public IngredientDataException(string message, Exception innerException) : base(message, innerException) { }
    }
}
