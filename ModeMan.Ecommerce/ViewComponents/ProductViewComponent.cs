using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModeMan.Ecommerce.Data;
using ModeMan.Ecommerce.Entities;

namespace ModeMan.Ecommerce.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly ModeManDbContext _context;

        public ProductViewComponent(ModeManDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string type)
        {
            if (type == "HotSales")
            {
                var bestSellers = await _context.Products
                    .OrderByDescending(p => p.OrderItems.Count)
                    .Take(8)
                    .ToListAsync();

                return View("HotSales", bestSellers);
            }
            else if (type == "NewArrivals")
            {
                //var newArrivals = await _context.Products.Take(4).ToListAsync();
                var newArrivals = await _context.Products.ToListAsync();


                return View("NewArrivals", newArrivals);
            }

            return View("Default");
        }

    }
}
