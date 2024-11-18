namespace API.Models.Domain
{
    public class Inventory
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
    }
}