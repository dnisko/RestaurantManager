using Common.Exceptions.Ingredient;
using Common.Exceptions.RecipeLine;
using Common.Exceptions.Server;
using Microsoft.AspNetCore.Mvc;
using Recipes.Controllers;
using Services.Interfaces;

namespace RestaurantManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CogsController : BaseController
    {
        private readonly ICogsService _cogsService;
        public CogsController(ICogsService cogsService)
        {
            _cogsService = cogsService;
        }
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetCogsAsync(int productId, [FromQuery] decimal marginPercent = 35, [FromQuery] int monthlyVolume = 1000)
        {
            try
            {
                var cogsResult = await _cogsService.CalculateCogsAsync(productId, marginPercent, monthlyVolume);
                return Response(cogsResult);
            }
            catch (RecipeLineNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (IngredientNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
