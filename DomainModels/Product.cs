namespace DomainModels
{
    public class Product : BaseEntity
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public bool IsActive { get; set; } = true;
        public ICollection<RecipeLine> RecipeLines { get; set; } = new List<RecipeLine>();

    }
}
