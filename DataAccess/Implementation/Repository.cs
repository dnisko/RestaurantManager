using DataAccess.Interfaces;
using DomainModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Implementation
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly RestaurantDbContext _context;
        private readonly DbSet<T> _table;

        public Repository(RestaurantDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }
        //public async Task<IEnumerable<T>> GetAllAsync()
        //{
        //    try
        //    {
        //        return await _table.AsNoTracking().ToListAsync();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _table.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync();
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _table.FindAsync(id);
        }
        public async Task AddAsync(T entity)
        {
            _table.Add(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return;

            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            _table.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
