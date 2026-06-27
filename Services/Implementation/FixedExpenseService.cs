using AutoMapper;
using Common.Exceptions.FixedExpense;
using Common.Responses;
using DataAccess.Interfaces;
using DomainModels;
using DTOs.Category;
using DTOs.FixedExpense;
using DTOs.Pagination;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace Services.Implementation
{
    public class FixedExpenseService : IFixedExpenseService
    {
        private readonly IFixedExpenseRepository _fixedExpenseRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<IFixedExpenseService> _logger;
        public FixedExpenseService(
            IFixedExpenseRepository fixedExpenseRepository,
            ILogger<IFixedExpenseService> logger,
            IMapper mapper
            )
        {
            _fixedExpenseRepository = fixedExpenseRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<CustomResponse<PaginatedResult<FixedExpenseDto>>> GetAllAsync(FixedExpensePaginationParams paginationParams)
        {
            try
            {
                var fixedExpenses = await _fixedExpenseRepository.GetPagedAsync(paginationParams);

                if (fixedExpenses.Items.Count == 0)
                {
                    return CustomResponse<PaginatedResult<FixedExpenseDto>>.Success(
                        new PaginatedResult<FixedExpenseDto>(new List<FixedExpenseDto>(), 0,
                        paginationParams.PageNumber, paginationParams.PageSize));
                }

                var mapped = _mapper.Map<List<FixedExpenseDto>>(fixedExpenses.Items);
                var result = new PaginatedResult<FixedExpenseDto>(mapped, fixedExpenses.TotalRecords,
                    paginationParams.PageNumber, paginationParams.PageSize);

                return CustomResponse<PaginatedResult<FixedExpenseDto>>.Success(result);
            }
            catch (FixedExpenseDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating a fixed expense.");
                throw new Exception("An unexpected error occurred while creating a fixed expense.");
            }
        }
        public async Task<CustomResponse<FixedExpenseDto>> GetByIdAsync(int id)
        {
            try
            {
                var fixedExpense = await _fixedExpenseRepository.GetByIdAsync(id);
                if (fixedExpense == null)
                {
                    throw new FixedExpenseNotFoundException($"Fixed expense with ID {id} not found.");
                }

                var mapped = _mapper.Map<FixedExpenseDto>(fixedExpense);
                return CustomResponse<FixedExpenseDto>.Success(mapped);
            }
            catch (FixedExpenseNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while getting a fixed expense.");
                throw new Exception("An unexpected error occurred while getting a fixed expense.");
            }
        }
        public async Task<CustomResponse<FixedExpenseDto>> CreateFixedExpenseAsync(CreateFixedExpenseDto fixedExpenseCreateDto)
        {
            try
            {
                var fixedExpenseEntity = _mapper.Map<FixedExpense>(fixedExpenseCreateDto);
                await _fixedExpenseRepository.AddAsync(fixedExpenseEntity);
                var mapped = _mapper.Map<FixedExpenseDto>(fixedExpenseEntity);
                return CustomResponse<FixedExpenseDto>.Success(mapped, "Fixed expense created successfully.");
            }
            catch (FixedExpenseDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating a fixed expense.");
                throw new Exception("An unexpected error occurred while creating a fixed expense.");
            }
        }
        public async Task<CustomResponse<FixedExpenseDto>> UpdateFixedExpenseAsync(UpdateFixedExpenseDto fixedExpenseUpdateDto)
        {
            try
            {
                var existingFixedExpense = await _fixedExpenseRepository.GetByIdAsync(fixedExpenseUpdateDto.Id);
                if (existingFixedExpense == null)
                {
                    throw new FixedExpenseNotFoundException($"Fixed expense with ID {fixedExpenseUpdateDto.Id} not found.");
                }

                _mapper.Map(fixedExpenseUpdateDto, existingFixedExpense);
                await _fixedExpenseRepository.UpdateAsync(existingFixedExpense);
                var mapped = _mapper.Map<FixedExpenseDto>(existingFixedExpense);
                return CustomResponse<FixedExpenseDto>.Success(mapped, "Fixed expense updated successfully.");
            }
            catch (FixedExpenseNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating a fixed expense.");
                throw new Exception("An unexpected error occurred while updating a fixed expense.");
            }
        }
        public async Task<CustomResponse> DeleteFixedExpenseAsync(int id)
        {
            try
            {
                var existingFixedExpense = await _fixedExpenseRepository.GetByIdAsync(id);
                if (existingFixedExpense == null)
                {
                    throw new FixedExpenseNotFoundException($"Fixed expense with ID {id} not found.");
                }
                await _fixedExpenseRepository.DeleteAsync(existingFixedExpense.Id);
                return CustomResponse.Success("Fixed expense deleted successfully.");
            }
            catch (FixedExpenseNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting a fixed expense.");
                throw new Exception("An unexpected error occurred while deleting a fixed expense.");
            }
        }

    }
}
