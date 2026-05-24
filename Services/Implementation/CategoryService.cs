using AutoMapper;
using Common.Exceptions.Category;
using Common.Responses;
using DataAccess.Interfaces;
using DomainModels;
using DTOs.Category;
using DTOs.Pagination;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<ICategoryService> _logger;
        private readonly IMapper _mapper;

        public CategoryService(
            ICategoryRepository categoryRepository,
            ILogger<ICategoryService> logger,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<CustomResponse<PaginatedResult<CategoryDto>>> GetAllAsync(CategoryPaginationParams paginationParams)
        {
            try
            {
                var categories = await _categoryRepository.GetPagedAsync(paginationParams);

                if (!categories.Items.Any())
                {
                    return CustomResponse<PaginatedResult<CategoryDto>>.Success(
                        new PaginatedResult<CategoryDto>(new List<CategoryDto>(), 0,
                        paginationParams.PageNumber, paginationParams.PageSize));
                }

                var mapped = _mapper.Map<List<CategoryDto>>(categories.Items);
                var result = new PaginatedResult<CategoryDto>(mapped, categories.TotalRecords,
                    paginationParams.PageNumber, paginationParams.PageSize);

                return CustomResponse<PaginatedResult<CategoryDto>>.Success(result);
            }
            catch (CategoryDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching categories.");
                throw new CategoryDataException("An error occurred while fetching categories.");
            }
        }
        public async Task<CustomResponse<CategoryDto>> GetByIdAsync(int id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);

                if (category == null)
                {
                    _logger.LogError($"Category with id {id} not found.");
                    throw new CategoryDataException($"Category with id {id} not found.");
                }

                var categoryDto = _mapper.Map<CategoryDto>(category);
                return CustomResponse<CategoryDto>.Success(categoryDto);

            }
            catch (CategoryDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching categories.");
                throw new CategoryDataException("An error occurred while fetching categories.");
            }
        }
        public async Task<CustomResponse<CategoryDto>> CreateCategoryAsync(CreateCategoryDto categoryCreateDto)
        {
            try
            {
                var categoryEntity = _mapper.Map<Category>(categoryCreateDto);
                await _categoryRepository.AddAsync(categoryEntity);

                var categoryDto = _mapper.Map<CategoryDto>(categoryEntity);
                return CustomResponse<CategoryDto>.Success(categoryDto, "Category created successfully.");
            }
            catch (CategoryDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating category.");
                throw new CategoryDataException("An error occurred while creating the category.");
            }
        }
        public async Task<CustomResponse<CategoryDto>> UpdateCategoryAsync(UpdateCategoryDto categoryUpdateDto)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(categoryUpdateDto.Id);
                if (category == null)
                {
                    _logger.LogError($"Category with id {categoryUpdateDto.Id} not found.");
                    throw new CategoryDataException($"Category \"{categoryUpdateDto.Name}\" not found.");
                }
                _mapper.Map(categoryUpdateDto, category);
                await _categoryRepository.UpdateAsync(category);
                var categoryDto = _mapper.Map<CategoryDto>(category);
                return CustomResponse<CategoryDto>.Success(categoryDto, "Category updated successfully.");
            }
            catch (CategoryDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating category.");
                throw new CategoryDataException("An error occurred while updating the category.");
            }
        }
        public async Task<CustomResponse> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if(category == null)
                {
                    _logger.LogError($"Category with id {id} not found.");
                    throw new CategoryDataException($"Category with id {id} not found.");
                }
                await _categoryRepository.DeleteAsync(category.Id);
                _logger.LogInformation($"Category with id {id} deleted successfully.");
                return CustomResponse.Success("Category deleted successfully.");
            }
            catch (CategoryDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting category.");
                throw new CategoryDataException("An error occurred while deleting the category.");
            }
        }
    }
}
