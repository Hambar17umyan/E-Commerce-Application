using API.Models.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Models.Domain.Concrete
{
    public class User : IdentityUser<int>, IDomain
    {
        public override int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public override string Email { get; set; }
        public override string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Role> Roles { get; set; }
        public ICollection<Order> Orders { get; set; }
        public Cart Cart { get; set; } = null;

    }
}
