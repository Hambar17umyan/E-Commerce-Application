using API.Data.Db;
using API.Data.Repositories.Interfaces;
using API.Models.Domain;
using FluentResults;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API.Data.Repositories.Concrete
{
    public sealed class RoleDataRepository : DataRepository<Role>, IRoleDataRepository
    {
        public RoleDataRepository(ECommerceDbContext context) : base(context, context.Roles) { }
    }
}
