using System.Collections.ObjectModel;

namespace E_Commerce.API.Models.DomainModels
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? HashedPassword { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Role>? Roles { get; set; }
        public ICollection<Order>? Orders { get; set; } = new List<Order>();
    }
}
