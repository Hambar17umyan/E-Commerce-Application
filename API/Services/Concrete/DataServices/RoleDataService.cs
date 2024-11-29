using API.Data.Repositories.Concrete;
using API.Data.Repositories.Interfaces;
using API.Models.Domain;
using API.Services.Interfaces.DataServices;
using FluentResults;

namespace API.Services.Concrete.DataServices
{
    public sealed class RoleDataService : DataService<Role>, IRoleDataService
    {
        private IConfiguration _configuration;

        public RoleDataService(IRoleDataRepository repo, IConfiguration configuration) : base(repo)
        {
            _configuration = configuration;
            _admin =  GetByIdAsync(int.Parse(_configuration["Roles:Admin"])).Result.Value;
            _customer = GetByIdAsync(int.Parse(_configuration["Roles:Customer"])).Result.Value;
        }

        private Role _admin { get; }
        private Role _customer { get; }

        public Role GetAdmin() => _admin;
        public Role GetCustomer() => _customer;
        public async Task<Result<Role>> GetByNameAsync(string name) => await GetByAsync(x => x.Name == name);
        public async Task<Result> RemoveAsync(string Name)
        {
            var resp = await GetByNameAsync(Name);
            if (resp.IsFailed)
                return Result.Fail(resp.Errors);

            var role = resp.Value;
            await RemoveAsync(role);

            return Result.Ok();
        }
    }
}
