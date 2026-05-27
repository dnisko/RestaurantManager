using Common.Helpers;
using Common.Responses;
using DataAccess.Interfaces;
using DomainModels;
using DTOs.Pagination;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementation
{
    public class IngredientRepository : Repository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(RestaurantDbContext context) : base(context)
        {
            
        }
        public async Task<PaginatedResult<Ingredient>> GetAllIngredientsAsync(IngredientPaginationParams paginationParams)
        {
            var query = _table.Include(p => p.Prices).ThenInclude(ip => ip.Supplier)
                .Include(p => p.RecipeLines).ThenInclude(rl => rl.Product)
                .AsQueryable();

            return await PaginationHelper.ApplyPaginationAsync(query, paginationParams);
        }

        public async Task<Ingredient?> GetIngredientWithDetailsAsync(int id)
        {
            return await _table.Include(p => p.Prices).ThenInclude(ip => ip.Supplier)
                .Include(p => p.RecipeLines).ThenInclude(rl => rl.Product)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
