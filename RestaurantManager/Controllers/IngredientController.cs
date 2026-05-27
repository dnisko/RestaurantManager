using Common.Exceptions.Ingredient;
using Common.Exceptions.Server;
using DTOs.Ingredient;
using DTOs.Pagination;
using Microsoft.AspNetCore.Mvc;
using Recipes.Controllers;
using Services.Interfaces;

namespace RestaurantManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : BaseController
    {
        private readonly IIngredientService _ingredientService;
        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync([FromQuery] IngredientPaginationParams paginationParams)
        {
            try
            {
                var result = await _ingredientService.GetAllAsync(paginationParams);
                return Response(result);
            }
            catch (IngredientDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("details")]
        public async Task<IActionResult> GetAllWithDetailsAsync([FromQuery] IngredientPaginationParams paginationParams)
        {
            try
            {
                var result = await _ingredientService.GetAllWithDetailsAsync(paginationParams);
                return Response(result);
            }
            catch (IngredientDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var result = await _ingredientService.GetByIdAsync(id);
                return Response(result);
            }
            catch (IngredientNotFoundException ex)
            {
                return NotFound(ex.Message); // 404 is more appropriate than 400 for "not found"
            }
            catch (IngredientDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateIngredientAsync([FromBody] CreateIngredientDto ingredientCreateDto)
        {
            try
            {
                var result = await _ingredientService.CreateIngredientAsync(ingredientCreateDto);
                return Response(result);
            }
            catch (IngredientDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("")]
        public async Task<IActionResult> UpdateIngredientAsync([FromBody] UpdateIngredientDto ingredientUpdateDto)
        {
            try
            {
                var result = await _ingredientService.UpdateIngredientAsync(ingredientUpdateDto);
                return Response(result);
            }
            catch (IngredientNotFoundException ex)
            {
                return NotFound(ex.Message); // 404 is more appropriate than 400 for "not found"
            }
            catch (IngredientDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredientAsync(int id)
        {
            try
            {
                var result = await _ingredientService.DeleteIngredientAsync(id);
                return Response(result);
            }
            catch (IngredientNotFoundException ex)
            {
                return NotFound(ex.Message); // 404 is more appropriate than 400 for "not found"
            }
            catch (IngredientDataException ex)
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