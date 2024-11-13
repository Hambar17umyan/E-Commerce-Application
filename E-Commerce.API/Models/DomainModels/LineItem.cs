namespace E_Commerce.API.Models.DomainModels
{
    public class LineItem
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public LineItem() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public LineItem(Order order, Product product, int quantity)
        {
            Order = order;
            Product = product;
            Quantity = quantity;
        }
        public int Id { get; set; }
        public Order Order { get; set; }
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
