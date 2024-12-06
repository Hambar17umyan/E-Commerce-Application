using API.Models.Domain.Interfaces;

namespace API.Models.Domain.Concrete
{
    public class Inventory : IDomain
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
    }
}