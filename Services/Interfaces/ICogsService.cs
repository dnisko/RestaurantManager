using Common.Responses;
using DTOs.COGS;

namespace Services.Interfaces
{
    public interface ICogsService
    {
        Task<CustomResponse<CogsResultDto>> CalculateCogsAsync(int productId, decimal marginPercent, int monthlyVolume);
    }
}
