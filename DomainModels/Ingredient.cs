namespace DomainModels
{
    public class Ingredient : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        //public bool IsActive { get; set; } = true;

        public ICollection<IngredientPrice> Prices { get; set; } = new List<IngredientPrice>();
        public ICollection<RecipeLine> RecipeLines { get; set; } = new List<RecipeLine>();
    }
}
