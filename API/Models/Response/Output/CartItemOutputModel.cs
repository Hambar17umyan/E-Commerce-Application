using API.Models.Domain.Concrete;

namespace API.Models.Response.Output
{
    public class CartItemOutputModel
    {
        public int Id { get; set; }
        public ProductOutputModel Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int CartId { get; set; }
    }
}
