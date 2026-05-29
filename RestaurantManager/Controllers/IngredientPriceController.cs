using Common.Exceptions.IngredientPrice;
using Common.Exceptions.Server;
using DTOs.IngredientPrice;
using DTOs.Pagination;
using Microsoft.AspNetCore.Mvc;
using Recipes.Controllers;
using Services.Interfaces;

namespace RestaurantManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientPriceController : BaseController
    {
        private readonly IIngredientPriceService _ingredientPriceService;

        public IngredientPriceController(IIngredientPriceService ingredientPriceService)
        {
            _ingredientPriceService = ingredientPriceService;
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActivePricesAsync()
        {
            try
            {
                var result = await _ingredientPriceService.GetAllActivePricesAsync();
                return Response(result);
            }
            catch (IngredientPriceDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{ingredientId}")]
        public async Task<IActionResult> GetActivePriceAsync(int ingredientId)
        {
            try
            {
                var result = await _ingredientPriceService.GetActivePriceAsync(ingredientId);
                return Response(result);
            }
            catch (IngredientPriceNotFoundDataException ex)
            {
                return NotFound(ex.Message);
            }
            catch (IngredientPriceDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{ingredientId}/history")]
        public async Task<IActionResult> GetPriceHistoryAsync(int ingredientId, [FromQuery] IngredientPricePaginationParams paginationParams)
        {
            try
            {
                var result = await _ingredientPriceService.GetPriceHistoryAsync(ingredientId, paginationParams);
                return Response(result);
            }
            catch (IngredientPriceNotFoundDataException ex)
            {
                return NotFound(ex.Message);
            }
            catch (IngredientPriceDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateIngredientPriceAsync([FromBody] CreateIngredientPriceDto ingredientPriceDto)
        {
            try
            {
                var result = await _ingredientPriceService.CreateIngredientPriceAsync(ingredientPriceDto);
                return Response(result);
            }
            catch (IngredientPriceDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("")]
        public async Task<IActionResult> UpdateIngredientPriceAsync([FromBody] UpdateIngredientPriceDto ingredientPriceDto)
        {
            try
            {
                var result = await _ingredientPriceService.UpdateIngredientPriceAsync(ingredientPriceDto);
                return Response(result);
            }
            catch (IngredientPriceDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{ingredientId}")]
        public async Task<IActionResult> DeleteIngredientPriceAsync(int ingredientId)
        {
            try
            {
                var result = await _ingredientPriceService.DeleteIngredientPriceAsync(ingredientId);
                return Response(result);
            }
            catch (IngredientPriceNotFoundDataException ex)
            {
                return NotFound(ex.Message);
            }
            catch (IngredientPriceDataException ex)
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