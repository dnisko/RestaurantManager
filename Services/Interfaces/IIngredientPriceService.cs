using Common.Responses;
using DTOs.IngredientPrice;
using DTOs.Pagination;

namespace Services.Interfaces
{
    public interface IIngredientPriceService
    {
        Task<CustomResponse<IEnumerable<IngredientPriceDto>>> GetAllActivePricesAsync(IngredientPricePaginationParams paginationParams);
        Task<CustomResponse<IngredientPriceDto>> GetActivePriceAsync(int id);
        Task<CustomResponse<IngredientPriceDto>> GetPriceHistoryAsync(int id, IngredientPricePaginationParams ingredientPriceCreateDto);
        Task<CustomResponse<IngredientPriceDto>> CreateCategoryAsync(CreateIngredientPriceDto ingredientPriceCreateDto);
        Task<CustomResponse<IngredientPriceDto>> UpdateCategoryAsync(UpdateIngredientPriceDto ingredientPriceUpdateDto);
        Task<CustomResponse> DeleteCategoryAsync(int id);
    }
}
