using System.Collections.ObjectModel;

namespace E_Commerce.API.Models.DomainModels
{
    internal class User
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public User() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        internal User(string name, string email, string hashedPassword, ICollection<Role> roles, bool isActive)
        {
            Name = name;
            Email = email;
            HashedPassword = hashedPassword;
            Roles = roles;
            IsActive = isActive;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Role> Roles { get; set; }
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
