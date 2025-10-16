using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModeMan.Ecommerce.Entities;

namespace ModeMan.Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UserController(
            UserManager<AppUser> userManager, 
            RoleManager<AppRole> roleManager
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var user = await _userManager.Users
                .Include(x => x.Orders)
                .FirstOrDefaultAsync(x => x.Id == id);

            ViewBag.Role = await _userManager.GetRolesAsync(user);

            return View(user);
        }
    }
}
