using API.Models.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace API.Models.Domain.Concrete
{
    public class Role : IdentityRole<int>, IDomain
    {
        public override int Id { get; set; }
        public override string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
    }
}