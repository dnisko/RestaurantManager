using Common.Exceptions.Ingredient;
using Common.Exceptions.RecipeLine;
using Common.Responses;
using DataAccess.Interfaces;
using DTOs.COGS;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace Services.Implementation
{
    public class CogsService : ICogsService
    {
        private readonly IRecipeLineRepository _recipeLineRepository;
        private readonly IFixedExpenseRepository _fixedExpenseRepository;
        private readonly IIngredientPriceRepository _ingredientPriceRepository;
        private readonly ILogger<ICogsService> _logger;

        public CogsService(
            IRecipeLineRepository recipeLineRepository,
            IFixedExpenseRepository fixedExpenseRepository,
            IIngredientPriceRepository ingredientPriceRepository,
            ILogger<ICogsService> logger)
        {
            _recipeLineRepository = recipeLineRepository;
            _fixedExpenseRepository = fixedExpenseRepository;
            _ingredientPriceRepository = ingredientPriceRepository;
            _logger = logger;
        }

        public async Task<CustomResponse<CogsResultDto>> CalculateCogsAsync(int productId, decimal marginPercent, int monthlyVolume)
        {
            try
            {
                var recipeLines = await _recipeLineRepository.GetRecipeLineByProductIdAsync(productId);
                var today = DateTime.UtcNow;
                var fixedCosts = await _fixedExpenseRepository.GetAllAsync(
                    fc => fc.ValidFrom <= today && (fc.ValidTo == null || fc.ValidTo >= today));
                //var fixedCosts = await _fixedExpenseRepository.GetAllAsync();
                var totalFixedCosts = fixedCosts.Sum(fc => fc.Amount);
                var fixedCostPerCup = monthlyVolume > 0 ? totalFixedCosts / monthlyVolume : 0;
                //var trueCostPerCup = ;
                if (recipeLines == null || !recipeLines.Any())
                {
                    throw new RecipeLineNotFoundException($"No recipe lines found for product ID {productId}.");
                }
                var cogsResult = new CogsResultDto
                {
                    ProductId = productId,
                    ProductName = recipeLines.First().Product?.Name ?? "Unknown",
                    Lines = new List<CogsLineDto>(),
                    TotalIngredientCost = 0,
                    MarginPercent = marginPercent,
                    SuggestedPrice = 0,
                    MarginAmount = 0,
                    TotalFixedCosts = totalFixedCosts,
                    FixedCostPerCup = fixedCostPerCup,
                    TrueCostPerCup = 0,
                    MonthlyVolume = monthlyVolume
                };
                foreach (var line in recipeLines)
                {
                    var ingredientPrice = await _ingredientPriceRepository.GetActivePriceAsync(line.IngredientId, DateTime.UtcNow);
                    if (ingredientPrice == null)
                    {
                        throw new IngredientNotFoundException($"No price found for ingredient ID {line.IngredientId}.");
                    }
                    var costPerUnit = ingredientPrice.Price / ingredientPrice.PriceQuantity;
                    var lineCost = line.Quantity * costPerUnit;
                    cogsResult.Lines.Add(new CogsLineDto
                    {
                        IngredientId = line.IngredientId,
                        IngredientName = line.Ingredient?.Name ?? "Unknown",
                        Quantity = line.Quantity,
                        Unit = line.Unit,
                        CostPerUnit = costPerUnit,
                        LineCost = lineCost
                    });
                    cogsResult.TotalIngredientCost += lineCost;
                }
                //cogsResult.MarginAmount = cogsResult.TotalIngredientCost * (marginPercent / 100);
                //cogsResult.SuggestedPrice = cogsResult.TotalIngredientCost + cogsResult.MarginAmount;

                //cogsResult.SuggestedPrice = cogsResult.TotalIngredientCost / (1 - marginPercent / 100);
                //cogsResult.MarginAmount = cogsResult.SuggestedPrice - cogsResult.TotalIngredientCost;

                cogsResult.TrueCostPerCup = cogsResult.TotalIngredientCost + fixedCostPerCup;
                cogsResult.SuggestedPrice = cogsResult.TrueCostPerCup / (1 - marginPercent / 100);
                cogsResult.MarginAmount = cogsResult.SuggestedPrice - cogsResult.TrueCostPerCup;

                return CustomResponse<CogsResultDto>.Success(cogsResult);
            }
            catch (RecipeLineNotFoundException)
            {
                throw;
            }
            catch (IngredientNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while calculating COGS.");
                throw new Exception("An error occurred while calculating COGS.");
            }
        }
    }
}
