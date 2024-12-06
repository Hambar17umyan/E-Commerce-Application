using API.Models.Domain.Concrete;
using FluentResults;

namespace API.Services.Interfaces.DataServices
{
    public interface IRoleDataService : IDataService<Role>
    {
        public Role GetAdmin();
        public Role GetCustomer();
        public Role GetSuperAdmin();
        public Result<Role> GetByName(string name);
        public Task<Result> RemoveAsync(string Name);

    }
}
