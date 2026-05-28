using AutoMapper;
using Common.Exceptions.Product;
using Common.Responses;
using DataAccess.Interfaces;
using DomainModels;
using DTOs.Pagination;
using DTOs.Product;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<IProductService> _logger;
        private readonly IMapper _mapper;

        public ProductService(
            IProductRepository productRepository,
            ILogger<IProductService> logger,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<CustomResponse<PaginatedResult<ProductDto>>> GetAllProductsAsync(ProductPaginationParams paginationParams)
        {
            try
            {
                var products = await _productRepository.GetAllProductsAsync(paginationParams);
                if (!products.Items.Any())
                {
                    return CustomResponse<PaginatedResult<ProductDto>>.Success(
                        new PaginatedResult<ProductDto>(new List<ProductDto>(), 0,
                        paginationParams.PageNumber, paginationParams.PageSize));
                }
                var mapped = _mapper.Map<List<ProductDto>>(products.Items);
                var result = new PaginatedResult<ProductDto>(mapped, products.TotalRecords,
                    paginationParams.PageNumber, paginationParams.PageSize);
                return CustomResponse<PaginatedResult<ProductDto>>.Success(result);
            }
            catch (ProductDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving products.");
                throw new ProductDataException("An error occurred while retrieving products.");
            }
        }
        public async Task<CustomResponse<ProductDto>> GetWithRecipeLinesAsync(int id)
        {
            try
            {
                var product = await _productRepository.GetWithRecipeLinesAsync(id);
                if (product == null)
                {
                    _logger.LogWarning("Product with id {Id} not found.", id);
                    throw new ProductNotFoundDataException($"Product with id {id} not found.");
                }

                var mapped = _mapper.Map<ProductDto>(product);
                return CustomResponse<ProductDto>.Success(mapped);
            }
            catch (ProductNotFoundDataException)
            {
                throw;
            }
            catch (ProductDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving products.");
                throw new Exception("An error occurred while retrieving products.");
            }
        }
        //public async Task<CustomResponse<ProductDto>> GetByIdAsync(int id)
        //{
        //    try
        //    {
        //        var product = await _productRepository.GetByIdAsync(id);
        //        if (product == null)
        //        {
        //            _logger.LogWarning("Product with id {Id} not found.", id);
        //            throw new ProductNotFoundDataException($"Product with id {id} not found.");
        //        }
        //        var mapped = _mapper.Map<ProductDto>(product);
        //        return CustomResponse<ProductDto>.Success(mapped);
        //    }
        //    catch (ProductNotFoundDataException)
        //    {
        //        throw;
        //    }
        //    catch (ProductDataException)
        //    {
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while retrieving products.");
        //        throw new ProductDataException("An error occurred while retrieving products.");
        //    }
        //}
        public async Task<CustomResponse<ProductDto>> CreateProductAsync(CreateProductDto createProductDto)
        {
            try
            {
                var product = _mapper.Map<Product>(createProductDto);
                await _productRepository.AddAsync(product);
                var productWithDetails = await _productRepository.GetWithRecipeLinesAsync(product.Id);
                var mapped = _mapper.Map<ProductDto>(productWithDetails);
                return CustomResponse<ProductDto>.Success(mapped, "Product created successfully.");
            }
            catch (ProductDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving products.");
                throw new ProductDataException("An error occurred while retrieving products.");
            }
        }
        public async Task<CustomResponse<ProductDto>> UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(updateProductDto.Id);
                if (product == null)
                {
                    _logger.LogWarning("Product with id {Id} not found.", updateProductDto.Id);
                    throw new ProductNotFoundDataException($"Product with id {updateProductDto.Id} not found.");
                }
                _mapper.Map(updateProductDto, product);
                await _productRepository.UpdateAsync(product);
                var productWithDetails = await _productRepository.GetWithRecipeLinesAsync(product.Id);
                var mapped = _mapper.Map<ProductDto>(productWithDetails);
                return CustomResponse<ProductDto>.Success(mapped, "Product updated successfully.");
            }
            catch (ProductDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving products.");
                throw new ProductDataException("An error occurred while retrieving products.");
            }
        }
        public async Task<CustomResponse> DeleteProductAsync(int id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                {
                    _logger.LogWarning("Product with id {Id} not found.", id);
                    throw new ProductNotFoundDataException($"Product with id {id} not found.");
                }
                await _productRepository.DeleteAsync(product.Id);
                return CustomResponse.Success("Product deleted successfully.");
            }
            catch (ProductNotFoundDataException)
            {
                throw;
            }
            catch (ProductDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving products.");
                throw new ProductDataException("An error occurred while retrieving products.");
            }
        }
    }
}
