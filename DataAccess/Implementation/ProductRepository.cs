using Common.Helpers;
using Common.Responses;
using DataAccess.Interfaces;
using DomainModels;
using DTOs.Pagination;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementation
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(RestaurantDbContext context) : base(context)
        {
        }

        public async Task<PaginatedResult<Product>> GetAllProductsAsync(ProductPaginationParams paginationParams)
        {
            var query = _table
                .Include(p => p.RecipeLines).ThenInclude(rl => rl.Ingredient)
                .Include(p => p.Category)
                .AsQueryable();

            return await PaginationHelper.ApplyPaginationAsync(query, paginationParams);
        }

        public async Task<Product?> GetWithRecipeLinesAsync(int id)
        {
            return await _table.Where(p => p.Id == id)
                .Include(p => p.Category)
                .Include(p => p.RecipeLines).ThenInclude(rl => rl.Ingredient)
                .FirstOrDefaultAsync();
        }
    }
}
