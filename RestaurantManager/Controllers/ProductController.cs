using Common.Exceptions.Product;
using Common.Exceptions.Server;
using DTOs.Pagination;
using DTOs.Product;
using Microsoft.AspNetCore.Mvc;
using Recipes.Controllers;
using Services.Interfaces;

namespace RestaurantManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllProductsAsync([FromQuery] ProductPaginationParams paginationParams)
        {
            try
            {
                var response = await _productService.GetAllProductsAsync(paginationParams);
                return Response(response);
            }
            catch (ProductDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWithRecipeLinesAsync(int id)
        {
            try
            {
                var response = await _productService.GetWithRecipeLinesAsync(id);
                return Response(response);
            }
            catch (ProductNotFoundDataException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ProductDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductDto createProductDto)
        {
            try
            {
                var response = await _productService.CreateProductAsync(createProductDto);
                return Response(response);
            }
            catch (ProductDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductAsync([FromBody] UpdateProductDto updateProductDto)
        {
            try
            {
                var response = await _productService.UpdateProductAsync(updateProductDto);
                return Response(response);
            }
            catch (ProductNotFoundDataException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ProductDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            try
            {
                var response = await _productService.DeleteProductAsync(id);
                return Response(response);
            }
            catch (ProductNotFoundDataException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ProductDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
