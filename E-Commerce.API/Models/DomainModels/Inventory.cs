namespace E_Commerce.API.Models.DomainModels
{
    internal class Inventory
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Inventory() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Inventory(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
        public int Id { get; set; }
        public Product? Product { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
