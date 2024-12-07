using API.Models.Domain.Concrete;
using API.Models.Response.Output;
using AutoMapper;

namespace API.AutoMapperProfiles
{
    public class CartItemProfile : Profile
    {
        public CartItemProfile()
        {
            CreateMap<CartItem, CartItemOutputModel>()
                .ForMember(dest => dest.Cart, opt => opt.MapFrom(src => src.Cart))
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));


            CreateMap<Cart, CartOutputModel>()
           .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        }
    }
}
