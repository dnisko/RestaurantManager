using AutoMapper;
using Common.Exceptions.Category;
using Common.Exceptions.IngredientPrice;
using Common.Responses;
using DataAccess.Implementation;
using DataAccess.Interfaces;
using DTOs.Category;
using DTOs.IngredientPrice;
using DTOs.Pagination;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace Services.Implementation
{
    public class IngredientPriceService : IIngredientPriceService
    {
        private readonly IIngredientPriceRepository _ingredientPriceRepository;
        private readonly ILogger<IIngredientPriceService> _logger;
        private readonly IMapper _mapper;

        public IngredientPriceService(
            IIngredientPriceRepository ingredientPriceRepository,
            ILogger<IIngredientPriceService> logger,
            IMapper mapper)
        {
            _ingredientPriceRepository = ingredientPriceRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<CustomResponse<IEnumerable<IngredientPriceDto>>> GetAllActivePricesAsync(IngredientPricePaginationParams paginationParams)
        {
            try
            {
                var ingredientPrice = await _ingredientPriceRepository.GetPagedAsync(paginationParams);
                if (!ingredientPrice.Items.Any())
                {
                    return CustomResponse<IEnumerable<IngredientPriceDto>>.Success(
                        new List<IngredientPriceDto> { });
                }
                var mapped = _mapper.Map<List<IngredientPriceDto>>(ingredientPrice.Items);
                var result = new List<IngredientPriceDto>(mapped);
                return CustomResponse<IEnumerable<IngredientPriceDto>>.Success(result);

            }
            catch(IngredientPriceDataException)
            {
                throw;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching ingredient prices.");
                throw new IngredientPriceDataException("An error occurred while fetching ingredient prices.");
            }
        }
        public async Task<CustomResponse<IngredientPriceDto>> GetActivePriceAsync(int id, DateTime today, IngredientPricePaginationParams paginationParams)
        {
            try
            {
                //var today = DateTime.UtcNow;
                var ingredientPrice = await _ingredientPriceRepository.GetActivePriceAsync(id, today, paginationParams);
                if(ingredientPrice == null)
                {
                    _logger.LogError($"Ingredient price with id {id} not found.");
                    throw new IngredientPriceNotFoundDataException($"Ingredient price with id {id} not found.");
                }
                var mapped = _mapper.Map<IngredientPriceDto>(ingredientPrice);
                return CustomResponse<IngredientPriceDto>.Success(mapped);
            }
            catch (IngredientPriceNotFoundDataException)
            {
                throw;
            }
            catch (IngredientPriceDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching ingredient prices.");
                throw new IngredientPriceDataException("An error occurred while fetching ingredient prices.");
            }
        }
        public async Task<CustomResponse<IngredientPriceDto>> GetPriceHistoryAsync(int id, IngredientPricePaginationParams ingredientPriceCreateDto)
        {
            try
            {
                var ingredientPrice = await _ingredientPriceRepository.GetPriceHistoryAsync(id, ingredientPriceCreateDto);
                if(ingredientPrice == null)
                {
                    _logger.LogError($"Ingredient price with id {id} not found.");
                    throw new IngredientPriceNotFoundDataException($"Ingredient price with id {id} not found.");
                }
                var mapped = _mapper.Map<IngredientPriceDto>(ingredientPrice);
                return CustomResponse<IngredientPriceDto>.Success(mapped);
            }
            catch (IngredientPriceNotFoundDataException)
            {
                throw;
            }
            catch (IngredientPriceDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching ingredient prices.");
                throw new IngredientPriceDataException("An error occurred while fetching ingredient prices.");
            }
        }
        public async Task<CustomResponse<IngredientPriceDto>> UpdateCategoryAsync(UpdateIngredientPriceDto ingredientPriceUpdateDto)
        {
            try
            {
                var ingredientPrice = await _ingredientPriceRepository.GetByIdAsync(ingredientPriceUpdateDto.Id);
                if (ingredientPrice == null)
                {
                    _logger.LogError($"Ingredient price with id {ingredientPriceUpdateDto.Id} not found.");
                    throw new CategoryDataException($"Ingredient price with id: \"{ingredientPriceUpdateDto.Id}\" not found.");
                }
                _mapper.Map(ingredientPriceUpdateDto, ingredientPrice);
                await _ingredientPriceRepository.UpdateAsync(ingredientPrice);
                var ingredientPriceDto = _mapper.Map<IngredientPriceDto>(ingredientPrice);
                return CustomResponse<IngredientPriceDto>.Success(ingredientPriceDto, "Category updated successfully.");
            }
            catch (IngredientPriceNotFoundDataException)
            {
                throw;
            }
            catch (IngredientPriceDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching ingredient prices.");
                throw new IngredientPriceDataException("An error occurred while fetching ingredient prices.");
            }
        }
        public Task<CustomResponse<IngredientPriceDto>> CreateCategoryAsync(CreateIngredientPriceDto ingredientPriceCreateDto)
        {
            try
            {

            }
            catch (IngredientPriceDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating ingredient prices.");
                throw new IngredientPriceDataException("An error occurred while creating ingredient prices.");
            }
        }
        public Task<CustomResponse> DeleteCategoryAsync(int id)
        {
            try
            {

            }
            catch (IngredientPriceNotFoundDataException)
            {
                throw;
            }
            catch (IngredientPriceDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting ingredient prices.");
                throw new IngredientPriceDataException("An error occurred while deleting ingredient prices.");
            }
        }

    }
}
