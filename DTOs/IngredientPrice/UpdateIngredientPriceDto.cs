namespace DTOs.IngredientPrice
{
    public class UpdateIngredientPriceDto
    {
        public int Id { get; set; }
        //public int IngredientId { get; set; }
        //public int SupplierId { get; set; }
        public decimal Price { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public bool IsPreferred { get; set; }
    }
}
