using E_Commerce.API.Models.DomainModels;

namespace E_Commerce.API.Services
{
    public class RoleManagementService
    {
        public Role GetAdmin()
        {
            //TODO..
            return new Role() { Name = "Admin", PriorityPoints = 1, Description = "aaa"};
        }
    }
}
