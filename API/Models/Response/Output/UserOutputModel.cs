using API.Models.Domain.Concrete;

namespace API.Models.Response.Output
{
    public class UserOutputModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public ICollection<RoleOutputModel> Roles { get; set; }
        public ICollection<OrderOutputModel> Orders { get; set; }
        public CartOutputModel Cart { get; set; } = null;
    }
}
