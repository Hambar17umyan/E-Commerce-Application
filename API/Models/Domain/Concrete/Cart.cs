using API.Models.Domain.Interfaces;

namespace API.Models.Domain.Concrete
{
    public class Cart : IDomain
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
