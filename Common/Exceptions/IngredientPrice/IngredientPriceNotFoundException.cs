namespace Common.Exceptions.IngredientPrice
{
    public class IngredientPriceNotFoundException : Exception
    {
        public IngredientPriceNotFoundException(string message) : base(message) { }
        public IngredientPriceNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
