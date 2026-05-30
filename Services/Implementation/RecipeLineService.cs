using AutoMapper;
using Common.Exceptions.RecipeLine;
using Common.Responses;
using DataAccess.Interfaces;
using DomainModels;
using DTOs.RecipeLine;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace Services.Implementation
{
    public class RecipeLineService : IRecipeLineService
    {
        private readonly IRecipeLineRepository _recipeLineRepository;
        private readonly ILogger<IRecipeLineService> _logger;
        private readonly IMapper _mapper;

        public RecipeLineService(
            IRecipeLineRepository recipeLineRepository,
            ILogger<IRecipeLineService> logger,
            IMapper mapper)
        {
            _recipeLineRepository = recipeLineRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<CustomResponse<IEnumerable<RecipeLineDto>>> GetRecipeLineByProductIdAsync(int productId)
        {
            try
            {
                var recipeLines = await _recipeLineRepository.GetRecipeLineByProductIdAsync(productId);
                if(!recipeLines.Any())
                {
                    return CustomResponse<IEnumerable<RecipeLineDto>>.Success([]);
                }
                var mapped = _mapper.Map<IEnumerable<RecipeLineDto>>(recipeLines);
                return CustomResponse<IEnumerable<RecipeLineDto>>.Success(mapped);
            }
            catch (RecipeLineNotFoundException)
            {
                throw;
            }
            catch (RecipeLineDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving recipe lines.");
                throw new Exception("An unexpected error occurred while retrieving recipe lines.");
            }
        }
        public async Task<CustomResponse<RecipeLineDto>> GetRecipeLineAsync(int productId, int ingredientId)
        {
            try
            {
                var recipeLine = await _recipeLineRepository.GetRecipeLineAsync(productId, ingredientId);
                if(recipeLine == null)
                {
                    _logger.LogWarning("Recipe line not found for ProductId: {ProductId} and IngredientId: {IngredientId}", productId, ingredientId);
                    throw new RecipeLineNotFoundException($"Recipe line not found for ProductId: {productId} and IngredientId: {ingredientId}");
                }
                var mapped = _mapper.Map<RecipeLineDto>(recipeLine);
                return CustomResponse<RecipeLineDto>.Success(mapped);
            }
            catch (RecipeLineNotFoundException)
            {
                throw;
            }
            catch (RecipeLineDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving recipe lines.");
                throw new Exception("An unexpected error occurred while retrieving recipe lines.");
            }
        }
        public async Task<CustomResponse<RecipeLineDto>> CreateRecipeLineAsync(CreateRecipeLineDto recipeLineCreateDto)
        {
            try
            {
                var recipeLineEntity = _mapper.Map<RecipeLine>(recipeLineCreateDto);
                var isValid = IsValidRecipeLine(recipeLineCreateDto);
                if (!isValid)
                {
                    return CustomResponse<RecipeLineDto>.Fail("Invalid recipe line data. Quantity must be greater than zero.");
                }

                await _recipeLineRepository.AddAsync(recipeLineEntity);
                var mapped = _mapper.Map<RecipeLineDto>(recipeLineEntity);
                return CustomResponse<RecipeLineDto>.Success(mapped, "Recipe line created successfully.");

            }
            catch (RecipeLineDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving recipe lines.");
                throw new Exception("An unexpected error occurred while retrieving recipe lines.");
            }
        }
        public async Task<CustomResponse<RecipeLineDto>> UpdateRecipeLineAsync(UpdateRecipeLineDto recipeLineUpdateDto)
        {
            try
            {
                var isValid = IsValidRecipeLine(recipeLineUpdateDto);
                if (!isValid)
                {
                    return CustomResponse<RecipeLineDto>.Fail("Invalid recipe line data. Quantity must be greater than zero.");
                }
                var recipeLine = await _recipeLineRepository.GetByIdAsync(recipeLineUpdateDto.Id);
                //var recipeLine = await _recipeLineRepository.GetRecipeLineAsync(recipeLineUpdateDto.ProductId, recipeLineUpdateDto.IngredientId);
                if (recipeLine == null)
                {
                    _logger.LogError($"Recipe line not found for ProductId: {recipeLineUpdateDto.ProductId} and IngredientId: {recipeLineUpdateDto.IngredientId}");
                    throw new RecipeLineNotFoundException($"Recipe line not found for ProductId: {recipeLineUpdateDto.ProductId} and IngredientId: {recipeLineUpdateDto.IngredientId}");
                }

                _mapper.Map(recipeLineUpdateDto, recipeLine);
                await _recipeLineRepository.UpdateAsync(recipeLine);
                var mapped = _mapper.Map<RecipeLineDto>(recipeLine);
                return CustomResponse<RecipeLineDto>.Success(mapped, "Recipe line updated successfully.");
            }
            catch (RecipeLineNotFoundException)
            {
                throw;
            }
            catch (RecipeLineDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving recipe lines.");
                throw new Exception("An unexpected error occurred while retrieving recipe lines.");
            }
        }
        public async Task<CustomResponse> DeleteRecipeLineAsync(int recipeLineId)
        {
            try
            {
                var recipeLine = await _recipeLineRepository.GetByIdAsync(recipeLineId);
                if (recipeLine == null)
                {
                    _logger.LogError($"Recipe line not found for Id: {recipeLineId}");
                    throw new RecipeLineNotFoundException($"Recipe line not found for Id: {recipeLineId}");
                }
                await _recipeLineRepository.DeleteAsync(recipeLineId);
                return CustomResponse.Success("Recipe line deleted successfully.");
            }
            catch (RecipeLineNotFoundException)
            {
                throw;
            }
            catch (RecipeLineDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving recipe lines.");
                throw new Exception("An unexpected error occurred while retrieving recipe lines.");
            }
        }

        private bool IsValidRecipeLine(CreateRecipeLineDto dto)
        {
            return dto.Quantity > 0;
        }
        private bool IsValidRecipeLine(UpdateRecipeLineDto dto)
        {
            return dto.Quantity > 0;
        }
        //private async Task<bool> IsValidRecipeLine(CheckQuantityRecipeLineDto checkQuantityRecipeLineDto)
        //{
        //    var checkQuantity = await _recipeLineRepository.GetByIdAsync(checkQuantityRecipeLineDto.Id);
        //    if (checkQuantity == null || checkQuantity.Quantity <= 0)
        //    {
        //        return checkQuantityRecipeLineDto.Quantity <= 0;//false?
        //    }

        //    return checkQuantityRecipeLineDto.Quantity > 0;//true?
        //}
    }
}
