using Common.Responses;
using DTOs.Pagination;
using DTOs.Supplier;

namespace Services.Interfaces
{
    public interface ISupplierService
    {
        Task<CustomResponse<PaginatedResult<SupplierDto>>> GetAllAsync(SupplierPaginationParams paginationParams);
        Task<CustomResponse<SupplierDto>> GetByIdAsync(int id);
        Task<CustomResponse<SupplierDto>> CreateSupplierAsync(CreateSupplierDto supplierCreateDto);
        Task<CustomResponse<SupplierDto>> UpdateSupplierAsync(UpdateSupplierDto supplierUpdateDto);
        Task<CustomResponse> DeleteSupplierAsync(int id);
    }
}
