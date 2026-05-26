using AutoMapper;
using Common.Exceptions.Supplier;
using Common.Responses;
using DataAccess.Interfaces;
using DomainModels;
using DTOs.Pagination;
using DTOs.Supplier;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace Services.Implementation
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ILogger<ISupplierService> _logger;
        private readonly IMapper _mapper;

        public SupplierService(
            ISupplierRepository supplierRepository,
            ILogger<ISupplierService> logger,
            IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<CustomResponse<PaginatedResult<SupplierDto>>> GetAllAsync(SupplierPaginationParams paginationParams)
        {
            try
            {
                var suppliers = await _supplierRepository.GetPagedAsync(paginationParams);
                if(!suppliers.Items.Any())
                {
                    return CustomResponse<PaginatedResult<SupplierDto>>.Success(
                        new PaginatedResult<SupplierDto>(new List<SupplierDto>(), 0,
                        paginationParams.PageNumber, paginationParams.PageSize));
                }
                var mapped = _mapper.Map<List<SupplierDto>>(suppliers.Items);
                var result = new PaginatedResult<SupplierDto>(mapped, suppliers.TotalRecords,
                    paginationParams.PageNumber, paginationParams.PageSize);
                return CustomResponse<PaginatedResult<SupplierDto>>.Success(result);
            }
            catch (SupplierDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching suppliers.");
                throw new SupplierDataException("An error occurred while fetching suppliers.");
            }
        }
        public async Task<CustomResponse<SupplierDto>> GetByIdAsync(int id)
        {
            try
            {
                var supplier = await _supplierRepository.GetByIdAsync(id);
                if (supplier == null)
                {
                    _logger.LogError($"Supplier with ID {id} not found.");
                    throw new SupplierNotFoundException($"Supplier with ID {id} not found.");
                }
                var mapped = _mapper.Map<SupplierDto>(supplier);
                return CustomResponse<SupplierDto>.Success(mapped);
            }
            catch (SupplierDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching suppliers.");
                throw new SupplierDataException("An error occurred while fetching suppliers.");
            }
        }
        public async Task<CustomResponse<SupplierDto>> CreateSupplierAsync(CreateSupplierDto supplierCreateDto)
        {
            try
            {
                var supplierEntity = _mapper.Map<Supplier>(supplierCreateDto);
                await _supplierRepository.AddAsync(supplierEntity);

                var supplierDto = _mapper.Map<SupplierDto>(supplierEntity);
                return CustomResponse<SupplierDto>.Success(supplierDto, "Supplier created successfully.");
            }
            catch (SupplierDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching suppliers.");
                throw new SupplierDataException("An error occurred while fetching suppliers.");
            }
        }
        public async Task<CustomResponse<SupplierDto>> UpdateSupplierAsync(UpdateSupplierDto supplierUpdateDto)
        {
            try
            {
                var supplier = await _supplierRepository.GetByIdAsync(supplierUpdateDto.Id);
                if (supplier == null)
                {
                    _logger.LogError($"Supplier with ID {supplierUpdateDto.Id} not found.");
                    throw new SupplierDataException($"Supplier \"{supplierUpdateDto.Name}\" not found.");
                }

                _mapper.Map(supplierUpdateDto, supplier);
                await _supplierRepository.UpdateAsync(supplier);
                var supplierDto = _mapper.Map<SupplierDto>(supplier);
                return CustomResponse<SupplierDto>.Success(supplierDto, "Supplier updated successfully.");
            }
            catch (SupplierDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching suppliers.");
                throw new SupplierDataException("An error occurred while fetching suppliers.");
            }
        }

        public async Task<CustomResponse> DeleteSupplierAsync(int id)
        {
            try
            {
                var supplier = await _supplierRepository.GetByIdAsync(id);
                if (supplier == null)
                {
                    _logger.LogError($"Supplier with ID {id} not found.");
                    throw new SupplierDataException($"Supplier with ID {id} not found.");
                }
                await _supplierRepository.DeleteAsync(supplier.Id);
                _logger.LogInformation($"Supplier with ID {id} deleted successfully.");
                return CustomResponse.Success("Supplier deleted successfully.");
            }
            catch (SupplierNotFoundException)
            {
                throw;
            }
            catch (SupplierDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching suppliers.");
                throw new SupplierDataException("An error occurred while fetching suppliers.");
            }
        }
    }
}
