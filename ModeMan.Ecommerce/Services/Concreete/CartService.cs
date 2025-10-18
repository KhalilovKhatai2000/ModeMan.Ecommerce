using Microsoft.EntityFrameworkCore;
using ModeMan.Ecommerce.Data;
using ModeMan.Ecommerce.Entities;
using ModeMan.Ecommerce.Services.Abstract;
using ModeMan.Ecommerce.Services.Common;

namespace ModeMan.Ecommerce.Services.Concreete
{
    public class CartService : Service<Cart>, ICartService
    {
        public CartService(ModeManDbContext context) : base(context)
        {
        }

        public async Task<string> AddToCartAsync(Guid userId, Guid productId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items).FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    Items = new List<CartItem>()
                };

                _context.Carts.Add(cart);
            }

            var cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Count += 1;
            }
            else
            {
                var newCartItem = new CartItem
                {
                    ProductId = productId,
                    Count = 1,
                    CartId = cart.Id
                };

                cart.Items.Add(newCartItem);

                _context.CartItems.Add(newCartItem);
            }
            await _context.SaveChangesAsync();

            return "";
        }

        public async Task UpdateCountAsync(Guid userId, Guid productId, int count)
        {
           var cart = await _context.Carts.FirstOrDefaultAsync(x => x.UserId == userId);

            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductId == productId && ci.CartId == cart.Id);

            if (cartItem != null)
            {
                cartItem.Count = count;
            }

            await _context.SaveChangesAsync();

        }

        public async Task<Cart> GetCartAsync(Guid userId)
        {
            return await _context.Carts
                .Include(c => c.Items)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId) 
                ?? 
                new Cart { 
                    Items = new List<CartItem>()
                };
        }
    }
}
