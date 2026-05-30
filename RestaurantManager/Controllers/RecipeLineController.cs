using Common.Exceptions.RecipeLine;
using Common.Exceptions.Server;
using DTOs.RecipeLine;
using Microsoft.AspNetCore.Mvc;
using Recipes.Controllers;
using Services.Interfaces;

namespace RestaurantManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeLineController : BaseController
    {
        private readonly IRecipeLineService _recipeLineService;
        public RecipeLineController(IRecipeLineService recipeLineService)
        {
            _recipeLineService = recipeLineService;
        }
        /*
        Task<CustomResponse<RecipeLineDto>> GetRecipeLineAsync(int productId, int ingredientId);
        Task<CustomResponse<IEnumerable<RecipeLineDto>>> GetRecipeLineByProductIdAsync(int productId);
        Task<CustomResponse<RecipeLineDto>> CreateRecipeLineAsync(CreateRecipeLineDto recipeLineCreateDto);
        Task<CustomResponse<RecipeLineDto>> UpdateRecipeLineAsync(UpdateRecipeLineDto recipeLineUpdateDto);
        Task<CustomResponse> DeleteRecipeLineAsync(int recipeLineId);
         */
        [HttpGet("{productId}/{ingredientId}")]
        public async Task<IActionResult> GetRecipeLineAsync(int productId, int ingredientId)
        {
            try
            {
                var response = await _recipeLineService.GetRecipeLineAsync(productId, ingredientId);
                return Response(response);

            }
            catch (RecipeLineNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (RecipeLineDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetRecipeLineByProductIdAsync(int productId)
        {
            try
            {
                var response = await _recipeLineService.GetRecipeLineByProductIdAsync(productId);
                return Response(response);
            }
            catch (RecipeLineNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (RecipeLineDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateRecipeLineAsync([FromBody] CreateRecipeLineDto recipeLineCreateDto)
        {
            try
            {
                var response = await _recipeLineService.CreateRecipeLineAsync(recipeLineCreateDto);
                return Response(response);
            }
            catch (RecipeLineDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("")]
        public async Task<IActionResult> UpdateRecipeLineAsync([FromBody] UpdateRecipeLineDto recipeLineUpdateDto)
        {
            try
            {
                var response = await _recipeLineService.UpdateRecipeLineAsync(recipeLineUpdateDto);
                return Response(response);
            }
            catch (RecipeLineDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{recipeLineId}")]
        public async Task<IActionResult> DeleteRecipeLineAsync(int recipeLineId)
        {
            try
            {
                var response = await _recipeLineService.DeleteRecipeLineAsync(recipeLineId);
                return Response(response);
            }
            catch (RecipeLineNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (RecipeLineDataException ex)
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
