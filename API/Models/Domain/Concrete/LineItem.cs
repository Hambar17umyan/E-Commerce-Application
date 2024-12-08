using API.Models.Domain.Interfaces;

namespace API.Models.Domain.Concrete
{
    public class LineItem : IDomain
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Product Product { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal OverallPriceAMD
        {
            get
            {
                ///TODO..
                return Quantity * Product?.Price ?? 0;
            }
            set
            {

            }
        }
    }
}