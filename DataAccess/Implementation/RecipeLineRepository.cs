using DataAccess.Interfaces;
using DomainModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementation
{
    public class RecipeLineRepository : Repository<RecipeLine>, IRecipeLineRepository
    {
        public RecipeLineRepository(RestaurantDbContext context) : base(context)
        {
        }
        public async Task<RecipeLine?> GetRecipeLineAsync(int productId, int ingredientId)
        {
            var result = await _table
                .Where(rl => rl.ProductId == productId
                && rl.IngredientId == ingredientId)
                .Include(ri => ri.Product)
                .Include(ri => ri.Ingredient)
                .Include(ri => ri.Ingredient.Prices)    
                .FirstOrDefaultAsync();
            return result;
        }
        public async Task<IEnumerable<RecipeLine>> GetRecipeLineByProductIdAsync(int productId)
        {
            var result = await _table
                .Where(rl => rl.ProductId == productId)
                .Include(ri => ri.Product)
                .Include(ri => ri.Ingredient)
                    .ThenInclude(i => i.Prices
                        .Where(p => p.IsPreferred
                            && p.ValidFrom <= DateTime.UtcNow
                            && (p.ValidTo == null || p.ValidTo >= DateTime.UtcNow)))
                .ToListAsync();
            return result;
        }
    }
}
