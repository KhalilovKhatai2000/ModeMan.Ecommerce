using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModeMan.Ecommerce.Services.Abstract;
using System.Security.Claims;

namespace ModeMan.Ecommerce.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var cart = await _cartService.GetCartAsync(userId);

            ViewBag.TotalPrice = cart.Items.
                Sum(ci => ci.TotalPrice);

            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid productId, int count)
        {
            Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await _cartService.AddToCartAsync(userId, productId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCount(Guid productId, int count)
        {
            Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await _cartService.UpdateCountAsync(userId, productId, count);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Remove(Guid id)
        {
            var cart = await _cartService.GetByIdAsync(id);
            _cartService.Delete(cart);
            return RedirectToAction("Index");
        }

    }
}
