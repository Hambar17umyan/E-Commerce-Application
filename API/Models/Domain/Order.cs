namespace API.Models.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int? UserId { get; set; }
        public ICollection<LineItem> LineItems { get; set; } = new List<LineItem>();
    }
}