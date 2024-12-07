using API.Models.Domain.Concrete;

namespace API.Models.Response.Output
{
    public class CartOutputModel
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public ICollection<CartItemOutputModel> Items { get; set; } = new List<CartItemOutputModel>();
    }
}
