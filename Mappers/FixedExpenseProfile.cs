using AutoMapper;
using DomainModels;
using DTOs.FixedExpense;

namespace Mappers
{
    public class FixedExpenseProfile : Profile
    {
        public FixedExpenseProfile()
        {
            CreateMap<FixedExpense, FixedExpenseDto>();
            CreateMap<CreateFixedExpenseDto, FixedExpense>();
            CreateMap<UpdateFixedExpenseDto, FixedExpense>();
        }
    }
}
