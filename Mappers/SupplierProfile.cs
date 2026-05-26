using AutoMapper;
using DomainModels;
using DTOs.Supplier;

namespace Mappers
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<Supplier, SupplierDto>();
            CreateMap<CreateSupplierDto, Supplier>();
            CreateMap<UpdateSupplierDto, Supplier>();
        }
    }
}
