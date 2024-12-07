using API.Models.Domain.Concrete;
using API.Models.Response.Output;
using AutoMapper;

namespace API.AutoMapperProfiles
{
    public class InventoryProfile : Profile
    {
        public InventoryProfile()
        {
            CreateMap<Inventory, InventoryOutputModel>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(inv => inv.Product));
        }
    }
}
