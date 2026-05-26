using DomainModels;

namespace DataAccess.Interfaces
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Task<Supplier?> GetByNameAsync(string name);
    }
}
