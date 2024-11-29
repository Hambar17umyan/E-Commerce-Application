using API.Models.Domain.Interfaces;

namespace API.Models.Domain
{
    public class CartItem : IDomain
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price
        {
            get
            {
                return Product is null ?
                    0 :
                    Product.Price * Quantity;
            }
            set
            {

            }
        }
        public Cart Cart { get; set; }
        public int CartId { get; set; }
    }
}