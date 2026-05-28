using Common.Responses;
using DTOs.Pagination;
using DTOs.Product;

namespace Services.Interfaces
{
    public interface IProductService
    {
        Task<CustomResponse<PaginatedResult<ProductDto>>> GetAllProductsAsync(ProductPaginationParams paginationParams);
        Task<CustomResponse<ProductDto>> GetWithRecipeLinesAsync(int id);
        //Task<CustomResponse<ProductDto>> GetByIdAsync(int id);
        Task<CustomResponse<ProductDto>> CreateProductAsync(CreateProductDto createProductDto);
        Task<CustomResponse<ProductDto>> UpdateProductAsync(UpdateProductDto updateProductDto);
        Task<CustomResponse> DeleteProductAsync(int id);
    }
}
