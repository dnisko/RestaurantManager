using Common.Responses;
using DTOs.IngredientPrice;
using DTOs.Pagination;

namespace Services.Interfaces
{
    public interface IIngredientPriceService
    {
        Task<CustomResponse<IEnumerable<IngredientPriceDto>>> GetAllActivePricesAsync();//IngredientPricePaginationParams paginationParams);
        Task<CustomResponse<IngredientPriceDto>> GetActivePriceAsync(int ingredientId);
        Task<CustomResponse<PaginatedResult<IngredientPriceDto>>> GetPriceHistoryAsync(int ingredientId, IngredientPricePaginationParams ingredientPriceCreateDto);
        Task<CustomResponse<IngredientPriceDto>> CreateIngredientPriceAsync(CreateIngredientPriceDto ingredientPriceCreateDto);
        Task<CustomResponse<IngredientPriceDto>> UpdateIngredientPriceAsync(UpdateIngredientPriceDto ingredientPriceUpdateDto);
        Task<CustomResponse> DeleteIngredientPriceAsync(int ingredientId);
    }
}
