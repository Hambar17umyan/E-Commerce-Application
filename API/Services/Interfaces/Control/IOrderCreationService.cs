using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;

namespace API.Services.Interfaces.Control
{
    public interface IOrderCreationService
    {
        Task<InnerResult<Order>> CreateOrderAsync(User user);
    }
}
