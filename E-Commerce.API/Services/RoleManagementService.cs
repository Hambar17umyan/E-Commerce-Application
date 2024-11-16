using E_Commerce.API.Models.DomainModels;

namespace E_Commerce.API.Services
{
    public class RoleManagementService
    {
        private static Role Admin = new Role() 
        {
            Name = "Admin", 
            Priority = 1, 
            Description = "Admin is the role with the greatest priority." 
        };
        private static Role Customer = new Role()
        {
            Name = "Customer",
            Priority = -1,
            Description = "Customer is the role of client."
        };
        public Role GetAdmin()
        {
            ///TODO..
            return Admin;
        }

        public Role GetCustomer()
        {
            ///TODO..
            return Customer;
        }
    }
}
