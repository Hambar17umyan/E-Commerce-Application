namespace API.Models.Response.Output
{
    public class InventoryOutputModel
    {
        public int Id { get; set; }
        public ProductOutputModel Product { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
