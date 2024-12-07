using API.Models.Domain.Concrete;
using API.Models.Response.Output;
using AutoMapper;

namespace API.AutoMapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductOutputModel>();
        }
    }
}
