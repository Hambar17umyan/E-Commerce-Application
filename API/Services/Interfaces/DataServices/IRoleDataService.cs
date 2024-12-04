using API.Models.Domain;
using FluentResults;

namespace API.Services.Interfaces.DataServices
{
    public interface IRoleDataService : IDataService<Role>
    {
        public Role GetAdmin();
        public Role GetCustomer();
        public Result<Role> GetByName(string name);
        public Task<Result> RemoveAsync(string Name);
    }
}
