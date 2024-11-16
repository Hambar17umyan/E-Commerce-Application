namespace E_Commerce.API.Models.DomainModels
{
    public class LineItem
    {
        public int Id { get; set; }
        public Order? Order { get; set; }
        public int OrderId { get; set; }
        public Product? Product { get; set; }
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
