using DataAccess.Interfaces;
using DomainModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementation
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        //private readonly RestaurantDbContext _context;
        public CategoryRepository(RestaurantDbContext context) : base(context)
        {
            //_context = context;
        }

        public async Task<Category?> GetByNameAsync(string name)
        {
            return await _table.FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}
