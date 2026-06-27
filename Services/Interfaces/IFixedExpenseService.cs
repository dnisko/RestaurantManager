using Common.Responses;
using DTOs.FixedExpense;
using DTOs.Pagination;

namespace Services.Interfaces
{
    public interface IFixedExpenseService
    {
        Task<CustomResponse<PaginatedResult<FixedExpenseDto>>> GetAllAsync(FixedExpensePaginationParams paginationParams);
        Task<CustomResponse<FixedExpenseDto>> GetByIdAsync(int id);
        Task<CustomResponse<FixedExpenseDto>> CreateFixedExpenseAsync(CreateFixedExpenseDto fixedExpenseCreateDto);
        Task<CustomResponse<FixedExpenseDto>> UpdateFixedExpenseAsync(UpdateFixedExpenseDto fixedExpenseUpdateDto);
        Task<CustomResponse> DeleteFixedExpenseAsync(int id);
    }
}
