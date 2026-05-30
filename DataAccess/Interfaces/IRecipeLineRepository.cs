using DomainModels;

namespace DataAccess.Interfaces
{
    public interface IRecipeLineRepository : IRepository<RecipeLine>
    {
        Task<RecipeLine?> GetRecipeLineAsync(int productId, int ingredientId);
        Task<IEnumerable<RecipeLine>> GetRecipeLineByProductIdAsync(int productId);
    }
}
