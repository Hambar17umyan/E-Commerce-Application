using API.Models.Control.ResultModels;
using API.Models.Domain.Concrete;

namespace API.Data.Repositories.Interfaces
{
    public interface ICartDataRepository : IDataRepository<Cart>
    {
        Task<InnerResult> AddToCartAsync(int id, Product product, int quantity);
        Task<InnerResult> RemoveFromCartAsync(int id, Product product, int? quantity = null);
    }
}
