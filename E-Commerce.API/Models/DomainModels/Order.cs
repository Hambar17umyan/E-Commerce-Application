namespace E_Commerce.API.Models.DomainModels
{
    internal class Order
    {
        public Order(User user)
        {
            User = user;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Order() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public int Id { get; set; }
        public User? User { get; set; }
        public int? UserId { get; set; }
        public ICollection<LineItem> LineItems { get; set; } = new List<LineItem>();
    }
}
