using Common.Responses;
using DomainModels;
using DTOs.Pagination;

namespace DataAccess.Interfaces
{
    public interface IIngredientRepository : IRepository<Ingredient>
    {
        Task<PaginatedResult<Ingredient>> GetAllIngredientsAsync(IngredientPaginationParams paginationParams);
        Task<Ingredient?> GetIngredientWithDetailsAsync(int id);
    }
}
