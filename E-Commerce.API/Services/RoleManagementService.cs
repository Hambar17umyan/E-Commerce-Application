using E_Commerce.API.Models.DomainModels;

namespace E_Commerce.API.Services
{
    public class RoleManagementService
    {
        private static Role Admin = new Role() { Name = "Admin", Priority = 1, Description = "aaa" };
        public Role GetAdmin()
        {
            ///TODO..
            return Admin;
        }
    }
}
