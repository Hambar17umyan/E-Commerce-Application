using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;

namespace API.Services.Interfaces.Control
{
    public interface ICartControllerService
    {
        public InnerResult<User> GetUserFromCart(Cart cart);
    }
}
