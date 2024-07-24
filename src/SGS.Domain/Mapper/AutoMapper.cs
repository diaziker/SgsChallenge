using AutoMapper;

namespace SGS.Domain.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Entities.EntityProduct, Contracts.Product>()
                .ForMember(dest => dest.Discount,
                                opt => opt.MapFrom(src => src.Discount))
                .ForMember(dest => dest.Id,
                                opt => opt.MapFrom(src => src.ProductId));

            CreateMap<Entities.EntityDiscount, Contracts.Discount>();
        }
    }
}
