using DataAccess.Interfaces;
using DomainModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementation
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(RestaurantDbContext context) : base(context)
        {
        }

        public async Task<Supplier?> GetByNameAsync(string name)
        {
            return await _table.FirstOrDefaultAsync(s => s.Name == name);
        }
    }
}
