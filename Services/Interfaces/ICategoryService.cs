using Common.Responses;
using DTOs.Category;
using DTOs.Pagination;

namespace Services.Interfaces
{
    public interface ICategoryService
    {

        Task<CustomResponse<PaginatedResult<CategoryDto>>> GetAllAsync(CategoryPaginationParams paginationParams);
        Task<CustomResponse<CategoryDto>> GetByIdAsync(int id);
        Task<CustomResponse<CategoryDto>> CreateCategoryAsync(CreateCategoryDto categoryCreateDto);
        Task<CustomResponse<CategoryDto>> UpdateCategoryAsync(UpdateCategoryDto categoryUpdateDto);
        Task<CustomResponse> DeleteCategoryAsync(int id);
        
    }
}
