using AutoMapper;
using DomainModels;
using DTOs.Product;

namespace Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<UpdateProductDto, Product>();
            CreateMap<CreateProductDto, Product>();
        }
    }
}
