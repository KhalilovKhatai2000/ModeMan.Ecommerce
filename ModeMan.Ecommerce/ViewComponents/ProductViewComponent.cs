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
                var bestSellers = ""; /*await _context.Products.ToListAsync();*/

                /*.OrderByDescending(p => p.OrderItems.Count)
                .Take(8)*/
                return View("HotSales", bestSellers);
            }
            else if (type == "NewArrivals")
            {
                var newArrivals = ""; /*await _context.Products.ToListAsync();*/
                /*.Take(4)*/

                return View("NewArrivals", newArrivals);
            }

            return View("Default");
        }

    }
}
