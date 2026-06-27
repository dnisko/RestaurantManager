using Common.Exceptions.FixedExpense;
using Common.Exceptions.Server;
using DTOs.FixedExpense;
using DTOs.Pagination;
using Microsoft.AspNetCore.Mvc;
using Recipes.Controllers;
using Services.Interfaces;

namespace RestaurantManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixedExpenseController : BaseController
    {
        private readonly IFixedExpenseService _fixedExpenseService;
        public FixedExpenseController(IFixedExpenseService fixedExpenseService)
        {
            _fixedExpenseService = fixedExpenseService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllFixedExpenses([FromQuery] FixedExpensePaginationParams paginationParams)
        {
            try
            {
                var fixedExpense = await _fixedExpenseService.GetAllAsync(paginationParams);
                return Response(fixedExpense);
            }
            catch (FixedExpenseNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (FixedExpenseDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFixedExpenseById(int id)
        {
            try
            {
                var fixedExpense = await _fixedExpenseService.GetByIdAsync(id);
                return Response(fixedExpense);
            }
            catch (FixedExpenseNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (FixedExpenseDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateFixedExpense([FromBody] CreateFixedExpenseDto fixedExpenseDto)
        {
            try
            {
                var fixedExpense = await _fixedExpenseService.CreateFixedExpenseAsync(fixedExpenseDto);
                return Response(fixedExpense);
            }
            catch (FixedExpenseNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (FixedExpenseDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateFixedExpense([FromBody] UpdateFixedExpenseDto fixedExpenseDto)
        {
            try
            {
                var fixedExpense = await _fixedExpenseService.UpdateFixedExpenseAsync(fixedExpenseDto);
                return Response(fixedExpense);
            }
            catch (FixedExpenseNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (FixedExpenseDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFixedExpense(int id)
        {
            try
            {
                var fixedExpense = await _fixedExpenseService.DeleteFixedExpenseAsync(id);
                return Response(fixedExpense);
            }
            catch (FixedExpenseNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (FixedExpenseDataException ex)
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
