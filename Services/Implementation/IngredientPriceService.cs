using AutoMapper;
using Common.Exceptions.IngredientPrice;
using Common.Responses;
using DataAccess.Interfaces;
using DomainModels;
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
        public async Task<CustomResponse<IEnumerable<IngredientPriceDto>>> GetAllActivePricesAsync()//IngredientPricePaginationParams paginationParams)
        {
            try
            {
                var ingredientPrice = await _ingredientPriceRepository.GetAllActivePricesAsync(DateTime.UtcNow);
                if (!ingredientPrice.Any())
                {
                    return CustomResponse<IEnumerable<IngredientPriceDto>>.Success(
                        new List<IngredientPriceDto> { });
                }
                var mapped = _mapper.Map<List<IngredientPriceDto>>(ingredientPrice);
                return CustomResponse<IEnumerable<IngredientPriceDto>>.Success(mapped);

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
        public async Task<CustomResponse<IngredientPriceDto>> GetActivePriceAsync(int ingredientId)
        {
            try
            {
                //var today = DateTime.UtcNow;
                var ingredientPrice = await _ingredientPriceRepository.GetActivePriceAsync(ingredientId, DateTime.UtcNow);
                if(ingredientPrice == null)
                {
                    _logger.LogError($"Ingredient price with id {ingredientId} not found.");
                    throw new IngredientPriceNotFoundDataException($"Ingredient price with id {ingredientId} not found.");
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
        public async Task<CustomResponse<PaginatedResult<IngredientPriceDto>>> GetPriceHistoryAsync(int ingredientId, IngredientPricePaginationParams ingredientPriceCreateDto)
        {
            try
            {
                var ingredientPrice = await _ingredientPriceRepository.GetPriceHistoryAsync(ingredientId, ingredientPriceCreateDto);
                if (!ingredientPrice.Items.Any())
                {
                    //_logger.LogError($"Ingredient price with id {ingredientId} not found.");
                    //throw new IngredientPriceNotFoundDataException($"Ingredient price with id {ingredientId} not found.");
                    return CustomResponse<PaginatedResult<IngredientPriceDto>>.Success(
                        new PaginatedResult<IngredientPriceDto>(new List<IngredientPriceDto>(), 0,
                        ingredientPriceCreateDto.PageNumber, ingredientPriceCreateDto.PageSize));
                }
                var mapped = _mapper.Map<List<IngredientPriceDto>>(ingredientPrice.Items);
                var result = new PaginatedResult<IngredientPriceDto>(mapped, ingredientPrice.TotalRecords,
                    ingredientPriceCreateDto.PageNumber, ingredientPriceCreateDto.PageSize);
                return CustomResponse<PaginatedResult<IngredientPriceDto>>.Success(result);
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
        public async Task<CustomResponse<IngredientPriceDto>> UpdateIngredientPriceAsync(UpdateIngredientPriceDto ingredientPriceUpdateDto)
        {
            try
            {
                //_logger.LogInformation("Updating ingredient price with id: {Id}", ingredientPriceUpdateDto.Id);
                var ingredientPrice = await _ingredientPriceRepository.GetByIdAsync(ingredientPriceUpdateDto.Id);
                if (ingredientPrice == null)
                {
                    _logger.LogError($"Ingredient price with id {ingredientPriceUpdateDto.Id} not found.");
                    throw new IngredientPriceNotFoundDataException($"Ingredient price with id: \"{ingredientPriceUpdateDto.Id}\" not found.");
                }
                _mapper.Map(ingredientPriceUpdateDto, ingredientPrice);
                await _ingredientPriceRepository.UpdateAsync(ingredientPrice);
                var ingredientPriceDto = _mapper.Map<IngredientPriceDto>(ingredientPrice);
                return CustomResponse<IngredientPriceDto>.Success(ingredientPriceDto, "Ingredient price updated successfully.");
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
        public async Task<CustomResponse<IngredientPriceDto>> CreateIngredientPriceAsync(CreateIngredientPriceDto ingredientPriceCreateDto)
        {
            try
            {
                var ingredientPrice = _mapper.Map<IngredientPrice>(ingredientPriceCreateDto);
                ingredientPrice.ValidFrom = ingredientPriceCreateDto.ValidFrom.Date;
                var checkExisting = await _ingredientPriceRepository.GetActivePriceAsync(
                    ingredientPrice.IngredientId, DateTime.UtcNow);
                if (checkExisting == null)
                {
                    _logger.LogInformation($"No active price found for ingredient with id: {ingredientPrice.IngredientId}. Adding new price");
                    //throw new IngredientPriceNotFoundDataException($"No active price found for ingredient with id {ingredientPrice.IngredientId}.");
                }
                else if (checkExisting.IngredientId == ingredientPrice.IngredientId)
                {
                    //await _ingredientPriceRepository.GetByIdAsync(checkExisting.Id);
                    checkExisting.ValidTo = DateTime.UtcNow;
                    await _ingredientPriceRepository.UpdateAsync(checkExisting);
                }
                
                await _ingredientPriceRepository.AddAsync(ingredientPrice);
                var ingredientPriceDto = _mapper.Map<IngredientPriceDto>(ingredientPrice);
                return CustomResponse<IngredientPriceDto>.Success(ingredientPriceDto, "Ingredient price created successfully.");

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
        public async Task<CustomResponse> DeleteIngredientPriceAsync(int ingredientId)
        {
            try
            {
                var ingredientPrice = await _ingredientPriceRepository.GetByIdAsync(ingredientId);
                if (ingredientPrice == null)
                {
                    _logger.LogError($"Ingredient price with id {ingredientId} not found.");
                    throw new IngredientPriceNotFoundDataException($"Ingredient price with id {ingredientId} not found.");
                }

                await _ingredientPriceRepository.DeleteAsync(ingredientPrice.Id);
                return CustomResponse.Success("Ingredient price deleted successfully.");
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
