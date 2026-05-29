namespace Common.Exceptions.IngredientPrice
{
    public class IngredientPriceDataException : Exception
    {
        public IngredientPriceDataException(string message) : base(message) { }
        public IngredientPriceDataException(string message, Exception innerException) : base(message, innerException) { }
    }
}
