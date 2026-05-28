using Common.Responses;
using DomainModels;
using DTOs.Pagination;

namespace DataAccess.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<PaginatedResult<Product>> GetAllProductsAsync(ProductPaginationParams paginationParams);
        //Task<PaginatedResult<Product>> GetByNameAsync(string name, ProductPaginationParams paginationParams);
        Task<Product?> GetWithRecipeLinesAsync(int id);
    }
}
