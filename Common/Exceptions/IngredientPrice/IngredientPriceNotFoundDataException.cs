namespace Common.Exceptions.IngredientPrice
{
    public class IngredientPriceNotFoundDataException : Exception
    {
        public IngredientPriceNotFoundDataException(string message) : base(message) { }
        public IngredientPriceNotFoundDataException(string message, Exception innerException) : base(message, innerException) { }
    }
}
