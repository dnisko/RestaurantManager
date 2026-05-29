using Common.Responses;
using DomainModels;
using DTOs.Pagination;

namespace DataAccess.Interfaces
{
    public interface IIngredientPriceRepository : IRepository<IngredientPrice>
    {
        Task<IEnumerable<IngredientPrice>> GetAllActivePricesAsync(DateTime today);
        Task<IngredientPrice?> GetActivePriceAsync(int ingredientId, DateTime today);//, IngredientPricePaginationParams paginationParams);
        Task<PaginatedResult<IngredientPrice>> GetPriceHistoryAsync(int ingredientId, IngredientPricePaginationParams paginationParams);
    }
}
