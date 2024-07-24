using AutoMapper;

namespace SGS.Domain.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Entities.EntityProduct, Contracts.Product>()
                .ForMember(dest => dest.Discount,
                                opt => opt.MapFrom(src => src.Discount));

            CreateMap<Entities.EntityDiscount, Contracts.Discount>();
        }
    }
}
