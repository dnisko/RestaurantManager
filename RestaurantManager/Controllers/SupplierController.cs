using Common.Exceptions.Server;
using Common.Exceptions.Supplier;
using DTOs.Pagination;
using DTOs.Supplier;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Recipes.Controllers;
using Services.Interfaces;

namespace RestaurantManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : BaseController
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllSuppliers([FromQuery] SupplierPaginationParams paginationParams)
        {
            try
            {
                var suppliers = await _supplierService.GetAllAsync(paginationParams);
                return Response(suppliers);
            }
            catch (SupplierDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplierById(int id)
        {
            try
            {
                var supplier = await _supplierService.GetByIdAsync(id);
                return Response(supplier);
            }
            catch (SupplierNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (SupplierDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateSupplier([FromBody] CreateSupplierDto supplierCreateDto)
        {
            try
            {
                var createdSupplier = await _supplierService.CreateSupplierAsync(supplierCreateDto);
                return Response(createdSupplier);
            }
            catch (SupplierDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("")]
        public async Task<IActionResult> UpdateSupplier([FromBody] UpdateSupplierDto supplierUpdateDto)
        {
            try
            {
                var updatedSupplier = await _supplierService.UpdateSupplierAsync(supplierUpdateDto);
                return Response(updatedSupplier);
            }
            catch (SupplierNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (SupplierDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            try
            {
                var deleteResponse = await _supplierService.DeleteSupplierAsync(id);
                return Response(deleteResponse);
            }
            catch (SupplierNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (SupplierDataException ex)
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