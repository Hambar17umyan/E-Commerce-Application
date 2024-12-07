using API.Models.Domain.Concrete;
using API.Models.Response.Output;
using AutoMapper;

namespace API.AutoMapperProfiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile() 
        {
            CreateMap<Role, RoleOutputModel>();
        }
    }
}
