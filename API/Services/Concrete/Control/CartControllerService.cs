using API.Data.Repositories.Interfaces;
using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;
using API.Services.Interfaces.Control;
using API.Services.Interfaces.DataServices;
using System.Net;

namespace API.Services.Concrete.Control
{
    public class CartControllerService : ICartControllerService
    {
        private IUserDataService _userDataService;

        public CartControllerService(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        public InnerResult<User> GetUserFromCart(Cart cart)
        {
            if (cart.UserId == null)
                return InnerResult<User>.Fail("The cart did not have a user!", HttpStatusCode.UnprocessableContent);

            return _userDataService.GetById(cart.UserId.Value);
        }
    }
}
