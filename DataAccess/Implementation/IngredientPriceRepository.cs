using Common.Helpers;
using Common.Responses;
using DataAccess.Interfaces;
using DomainModels;
using DTOs.Pagination;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementation
{
    public class IngredientPriceRepository : Repository<IngredientPrice>, IIngredientPriceRepository
    {
        public IngredientPriceRepository(RestaurantDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<IngredientPrice>> GetAllActivePricesAsync(DateTime today)
        {
            return await _table
                .Include(ip => ip.Ingredient)
                .Include(ip => ip.Supplier)
                .Where(ip => ip.IsPreferred
                    && ip.ValidFrom <= today
                    && (ip.ValidTo == null || ip.ValidTo >= today))
                .ToListAsync();
        }
        public async Task<IngredientPrice?> GetActivePriceAsync(int ingredientId, DateTime today)//, IngredientPricePaginationParams paginationParams)
        {
            var query = await _table.Where(ip => ip.IngredientId == ingredientId
                && ip.IsPreferred
                && ip.ValidFrom <= today
                && (ip.ValidTo == null || ip.ValidTo >= today))
                .Include(i => i.Ingredient)
                .Include(s => s.Supplier)
                .FirstOrDefaultAsync();

            return query;
        }
        public async Task<PaginatedResult<IngredientPrice>> GetPriceHistoryAsync(int ingredientId, IngredientPricePaginationParams paginationParams)
        {
            var query = _table.Where(ip => ip.IngredientId == ingredientId
                && ip.IsPreferred)
                .Include(i => i.Ingredient)
                .Include(s => s.Supplier);
            //&& ip.ValidFrom <= today
            //&& (ip.ValidTo == null || ip.ValidTo >= today));

            return await PaginationHelper.ApplyPaginationAsync(query, paginationParams);
        }

    }
}
