using AutoMapper;
using DomainModels;
using DTOs.FixedExpense;

namespace Mappers
{
    public class FixedExpenseProfile : Profile
    {
        public FixedExpenseProfile()
        {
            CreateMap<FixedExpense, FixedExpenseDto>()
                .ForMember(dest => dest.Category,
                opt => opt.MapFrom(src => src.Category.ToString()));
            CreateMap<CreateFixedExpenseDto, FixedExpense>();
            CreateMap<UpdateFixedExpenseDto, FixedExpense>();
        }
    }
}
