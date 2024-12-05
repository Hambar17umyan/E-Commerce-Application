using API.Models.Domain.Interfaces;

namespace API.Models.Domain
{
    public class Product : IDomain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}