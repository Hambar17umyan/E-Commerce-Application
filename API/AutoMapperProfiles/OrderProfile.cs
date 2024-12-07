using API.Models.Domain.Concrete;
using API.Models.Response.Output;
using AutoMapper;

namespace API.AutoMapperProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile() 
        {
            CreateMap<Order, OrderOutputModel>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.LineItems, opt => opt.MapFrom(src => src.LineItems));
        }
    }
}
