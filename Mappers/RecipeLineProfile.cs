using AutoMapper;
using DomainModels;
using DTOs.RecipeLine;

namespace Mappers
{
    public class RecipeLineProfile : Profile
    {
        public RecipeLineProfile()
        {
            CreateMap<RecipeLine, RecipeLineDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Ingredient.Name));
            CreateMap<CreateRecipeLineDto, RecipeLine>();
            CreateMap<UpdateRecipeLineDto, RecipeLine>();
        }
    }
}
