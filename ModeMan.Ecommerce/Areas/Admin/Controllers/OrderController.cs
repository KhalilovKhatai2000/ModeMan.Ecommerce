using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModeMan.Ecommerce.Entities;
using ModeMan.Ecommerce.Enums;
using ModeMan.Ecommerce.Services.Abstract;

namespace ModeMan.Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            List<Order> Orders = _orderService.GetAll()
                                                .Include(x => x.AppUser)
                                                .ToList();


            return View(Orders);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            Order order = await _orderService.GetByExpressionAsync(
                x => x.Id == id, 
                x => x.AppUser, 
                x => x.OrderItems, 
                x => x.OrderItems.Select(or => or.Product));

            return View(order);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            _orderService.Delete(_orderService.GetById(id));

            return RedirectToAction("Index");
        }
    }
}
