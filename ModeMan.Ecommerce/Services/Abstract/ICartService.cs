using ModeMan.Ecommerce.Entities;
using ModeMan.Ecommerce.Services.Common;

namespace ModeMan.Ecommerce.Services.Abstract
{
    public interface ICartService : IService<Cart>
    {
        Task<string> AddToCartAsync(Guid userId, Guid productId);
        Task<Cart> GetCartAsync(Guid userId);
        Task UpdateCountAsync(Guid userId, Guid productId, int count);
    }
}
