using AutoMapper;
using DomainModels;
using DTOs.Ingredient;

namespace Mappers
{
    public class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<Ingredient, IngredientDto>();
            CreateMap<CreateIngredientDto, Ingredient>();
            CreateMap<UpdateIngredientDto, Ingredient>();
        }
    }
}
