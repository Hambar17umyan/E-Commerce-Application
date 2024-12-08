using API.Models.Domain.Interfaces;
using System.Text.Json.Serialization;

namespace API.Models.Domain.Concrete
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

        public int CartId { get; set; }

    }
}