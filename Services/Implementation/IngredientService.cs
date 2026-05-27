using AutoMapper;
using Common.Exceptions.Ingredient;
using Common.Responses;
using DataAccess.Interfaces;
using DomainModels;
using DTOs.Ingredient;
using DTOs.Pagination;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace Services.Implementation
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly ILogger<IngredientService> _logger;
        private readonly IMapper _mapper;
        public IngredientService(
            IIngredientRepository ingredientRepository,
            ILogger<IngredientService> logger,
            IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<CustomResponse<PaginatedResult<IngredientDto>>> GetAllAsync(IngredientPaginationParams paginationParams)
        {
            try
            {
                var ingredients = await _ingredientRepository.GetPagedAsync(paginationParams);
                if(!ingredients.Items.Any())
                {
                    return CustomResponse<PaginatedResult<IngredientDto>>.Success(
                        new PaginatedResult<IngredientDto>(new List<IngredientDto>(), 0,
                        paginationParams.PageNumber, paginationParams.PageSize));
                }
                var mapped = _mapper.Map<List<IngredientDto>>(ingredients.Items);
                var result = new PaginatedResult<IngredientDto>(mapped, ingredients.TotalRecords,
                    paginationParams.PageNumber, paginationParams.PageSize);
                return CustomResponse<PaginatedResult<IngredientDto>>.Success(result);
            }
            catch (IngredientDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving ingredients.");
                throw new Exception("An unexpected error occurred while retrieving ingredients.");
            }
        }
        public async Task<CustomResponse<PaginatedResult<IngredientDto>>> GetAllWithDetailsAsync(IngredientPaginationParams paginationParams)
        {
            try
            {
                var ingredients = await _ingredientRepository.GetAllIngredientsAsync(paginationParams);
                if (!ingredients.Items.Any())
                {
                    return CustomResponse<PaginatedResult<IngredientDto>>.Success(
                        new PaginatedResult<IngredientDto>(new List<IngredientDto>(), 0,
                        paginationParams.PageNumber, paginationParams.PageSize));
                }
                var mapped = _mapper.Map<List<IngredientDto>>(ingredients.Items);
                var result = new PaginatedResult<IngredientDto>(mapped, ingredients.TotalRecords,
                    paginationParams.PageNumber, paginationParams.PageSize);
                return CustomResponse<PaginatedResult<IngredientDto>>.Success(result);
            }
            catch (IngredientDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving ingredients.");
                throw new Exception("An unexpected error occurred while retrieving ingredients.");
            }
        }
        public async Task<CustomResponse<IngredientDto>> GetByIdAsync(int id)
        {
            try
            {
                var ingredient = await _ingredientRepository.GetIngredientWithDetailsAsync(id);
                if (ingredient == null)
                {
                    _logger.LogWarning("Ingredient with ID {Id} not found.", id);
                    throw new IngredientNotFoundException($"Ingredinet with ID {id} not found.");
                }
                var mapped = _mapper.Map<IngredientDto>(ingredient);
                var result = CustomResponse<IngredientDto>.Success(mapped);
                return result;
            }
            catch (IngredientNotFoundException)
            {
                throw;
            }
            catch (IngredientDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving ingredients.");
                throw new Exception("An unexpected error occurred while retrieving ingredients.");
            }
        }
        public async Task<CustomResponse<IngredientDto>> CreateIngredientAsync(CreateIngredientDto ingredientCreateDto)
        {
            try
            {
                var ingredinetEntity = _mapper.Map<Ingredient>(ingredientCreateDto);
                ingredinetEntity.QuantityOnHand = 0;
                //ingredinetEntity.MinimumQuantity = 0;
                await _ingredientRepository.AddAsync(ingredinetEntity);

                var ingredinet = _mapper.Map<IngredientDto>(ingredinetEntity);
                return CustomResponse<IngredientDto>.Success(ingredinet, "Ingredient created successfully.");
            }
            catch (IngredientDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving ingredients.");
                throw new Exception("An unexpected error occurred while retrieving ingredients.");
            }
        }
        public async Task<CustomResponse<IngredientDto>> UpdateIngredientAsync(UpdateIngredientDto ingredientUpdateDto)
        {
            try
            {
                var ingredient = await _ingredientRepository.GetByIdAsync(ingredientUpdateDto.Id);
                if (ingredient == null)
                {
                    _logger.LogWarning("Ingredient with ID {Id} not found.", ingredientUpdateDto.Id);
                    throw new IngredientNotFoundException($"Ingredient with ID {ingredientUpdateDto.Id} not found.");
                }
                _mapper.Map(ingredientUpdateDto, ingredient);
                await _ingredientRepository.UpdateAsync(ingredient);
                var ingredinetDto = _mapper.Map<IngredientDto>(ingredient);
                return CustomResponse<IngredientDto>.Success(ingredinetDto, "Ingredient updated successfully.");
            }
            catch (IngredientNotFoundException)
            {
                throw;
            }
            catch (IngredientDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving ingredients.");
                throw new Exception("An unexpected error occurred while retrieving ingredients.");
            }
        }
        public async Task<CustomResponse> DeleteIngredientAsync(int id)
        {
            try
            {
                var ingredient = await _ingredientRepository.GetByIdAsync(id);
                if (ingredient == null)
                {
                    _logger.LogWarning("Ingredient with ID {Id} not found.", id);
                    throw new IngredientNotFoundException($"Ingredient with ID {id} not found.");
                }
                await _ingredientRepository.DeleteAsync(ingredient.Id);
                _logger.LogInformation($"Ingredient with ID {id} deleted successfully.");
                return CustomResponse.Success("Ingredient deleted successfully.");
            }

            catch (IngredientNotFoundException)
            {
                throw;
            }
            catch (IngredientDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving ingredients.");
                throw new Exception("An unexpected error occurred while retrieving ingredients.");
            }
        }
    }
}
