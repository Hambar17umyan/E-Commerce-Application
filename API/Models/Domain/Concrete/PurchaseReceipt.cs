using API.Models.Domain.Interfaces;
using API.Models.Response.Output;

namespace API.Models.Domain.Concrete
{
    public class PurchaseReceipt : IDomain
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int? UserId
        {
            get
            {
                return Order.UserId;
            }
            set
            {
            }
        }
        public int? OrderId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public OrderOutputModel Order { get; set; }
    }
}
