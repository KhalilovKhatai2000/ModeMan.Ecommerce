using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModeMan.Ecommerce.Data;
using ModeMan.Ecommerce.Entities;

namespace ModeMan.Ecommerce.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly ModeManDbContext _context;

        public SliderViewComponent(ModeManDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Slider>? sliders = await _context.Sliders.ToListAsync();
            return View(sliders);
        }
    }
}
