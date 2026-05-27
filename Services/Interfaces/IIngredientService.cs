using Common.Responses;
using DTOs.Ingredient;
using DTOs.Pagination;

namespace Services.Interfaces
{
    public interface IIngredientService
    {
        Task<CustomResponse<PaginatedResult<IngredientDto>>> GetAllAsync(IngredientPaginationParams paginationParams);
        Task<CustomResponse<PaginatedResult<IngredientDto>>> GetAllWithDetailsAsync(IngredientPaginationParams paginationParams);
        Task<CustomResponse<IngredientDto>> GetByIdAsync(int id);
        Task<CustomResponse<IngredientDto>> CreateIngredientAsync(CreateIngredientDto ingredientCreateDto);
        Task<CustomResponse<IngredientDto>> UpdateIngredientAsync(UpdateIngredientDto ingredientUpdateDto);
        Task<CustomResponse> DeleteIngredientAsync(int id);
    }
}
