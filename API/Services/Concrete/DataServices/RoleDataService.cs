using API.Data.Repositories.Concrete;
using API.Data.Repositories.Interfaces;
using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
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
            _admin = GetById(int.Parse(_configuration["Roles:Admin"])).Value;
            _customer = GetById(int.Parse(_configuration["Roles:Customer"])).Value;
            _superAdmin = GetById(int.Parse(_configuration["Roles:SuperAdmin"])).Value;
        }

        private Role _admin { get; }
        private Role _customer { get; }
        private Role _superAdmin { get; }

        public Role GetAdmin() => _admin;
        public Role GetCustomer() => _customer;
        public Role GetSuperAdmin() => _superAdmin;
        public InnerResult<Role> GetByName(string name) => GetBy(x => x.Name == name);
        public async Task<InnerResult> RemoveAsync(string Name)
        {
            var resp = GetByName(Name);
            if (resp.IsFailed)
                return InnerResult.Fail(resp.Errors, resp.StatusCode);

            var role = resp.Value;
            var res = await RemoveAsync(role);
            if (res.IsFailed)
                return res;

            return InnerResult.Ok();
        }

    }
}
