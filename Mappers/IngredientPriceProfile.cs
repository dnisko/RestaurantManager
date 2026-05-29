using AutoMapper;
using DomainModels;
using DTOs.IngredientPrice;

namespace Mappers
{
    public class IngredientPriceProfile : Profile
    {
        public IngredientPriceProfile()
        {
            CreateMap<IngredientPrice, IngredientPriceDto>();
            CreateMap<CreateIngredientPriceDto, IngredientPrice>();
            CreateMap<UpdateIngredientPriceDto, IngredientPrice>();
        }
    }
}
