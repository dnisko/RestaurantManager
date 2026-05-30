using Common.Responses;
using DTOs.RecipeLine;

namespace Services.Interfaces
{
    public interface IRecipeLineService
    {
        Task<CustomResponse<RecipeLineDto>> GetRecipeLineAsync(int productId, int ingredientId);
        Task<CustomResponse<IEnumerable<RecipeLineDto>>> GetRecipeLineByProductIdAsync(int productId);
        Task<CustomResponse<RecipeLineDto>> CreateRecipeLineAsync(CreateRecipeLineDto recipeLineCreateDto);
        Task<CustomResponse<RecipeLineDto>> UpdateRecipeLineAsync(UpdateRecipeLineDto recipeLineUpdateDto);
        Task<CustomResponse> DeleteRecipeLineAsync(int recipeLineId);
    }
}
