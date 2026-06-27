using Common.Responses;
using DomainModels;
using DTOs.Pagination;

namespace DataAccess.Interfaces
{
    public interface IFixedExpenseRepository : IRepository<FixedExpense>
    {
        //Task<PaginatedResult<FixedExpense>> GetAllFixedExpensesAsync(FixedExpensePaginationParams paginationParams);
    }
}
