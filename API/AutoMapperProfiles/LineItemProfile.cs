using API.Models.Domain.Concrete;
using API.Models.Response.Output;
using AutoMapper;

namespace API.AutoMapperProfiles
{
    public class LineItemProfile : Profile
    {
        public LineItemProfile()
        {
            CreateMap<LineItem, LineItemOutputModel>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));
        }
    }
}
