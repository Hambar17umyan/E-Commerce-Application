namespace E_Commerce.API.Models.DomainModels
{
    public class Inventory
    {
        public int Id { get; set; }
        public Product? Product { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
