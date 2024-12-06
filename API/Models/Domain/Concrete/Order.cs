using API.Models.Domain.Interfaces;

namespace API.Models.Domain.Concrete
{
    public class Order : IDomain
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int? UserId { get; set; }
        public ICollection<LineItem> LineItems { get; set; } = new List<LineItem>();
    }
}