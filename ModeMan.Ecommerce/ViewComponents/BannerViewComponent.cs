using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModeMan.Ecommerce.Data;
using ModeMan.Ecommerce.Entities;

namespace ModeMan.Ecommerce.ViewComponents
{
    public class BannerViewComponent : ViewComponent
    {
        private readonly ModeManDbContext _context;

        public BannerViewComponent(ModeManDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Banner>? banners = await _context.Banners.ToListAsync();
            return View(banners);
        }
    }
}
