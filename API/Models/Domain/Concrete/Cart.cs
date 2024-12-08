using API.Models.Domain.Interfaces;

namespace API.Models.Domain.Concrete
{
    public class Cart : IDomain
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public ICollection<CartItem> Items { get; set; }


        public static bool operator ==(Cart a, Cart b) => a.Id == b.Id;
        public static bool operator !=(Cart a, Cart b) => a.Id != b.Id;

        public override bool Equals(object obj)
        {
            var cart = obj as Cart;
            if (cart is null)
                return false;
            return cart == this;
        }

        public override int GetHashCode()
        {
            var hash = HashCode.Combine(Id);
            return hash;
        }
    }
}
