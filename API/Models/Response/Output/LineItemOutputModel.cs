namespace API.Models.Response.Output
{
    public class LineItemOutputModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public ProductOutputModel Product { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal OverallPriceAMD { get; set; }
    }
}
