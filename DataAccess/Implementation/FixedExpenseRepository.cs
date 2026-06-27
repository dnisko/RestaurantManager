using Common.Responses;
using DataAccess.Interfaces;
using DomainModels;
using DTOs.Pagination;

namespace DataAccess.Implementation
{
    public class FixedExpenseRepository : Repository<FixedExpense>, IFixedExpenseRepository
    {
        public FixedExpenseRepository(RestaurantDbContext context) : base(context)
        {
            
        }
        //public Task<PaginatedResult<FixedExpense>> GetAllFixedExpensesAsync(FixedExpensePaginationParams paginationParams)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
