using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModeMan.Ecommerce.Entities;
using ModeMan.Ecommerce.Services.Abstract;

namespace ModeMan.Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService CartService)
        {
            _cartService = CartService;
        }

        public IActionResult Index()
        {
            List<Cart> carts = _cartService.GetAll()
                                                .Include(x => x.AppUser)
                                                .ToList();


            return View(carts);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            _cartService.Delete(_cartService.GetById(id));

            return RedirectToAction("Index");
        }
    }
}
