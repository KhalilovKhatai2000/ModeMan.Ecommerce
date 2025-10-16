using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModeMan.Ecommerce.Entities;

namespace ModeMan.Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _roleManager.Roles.ToListAsync());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(AppRole appRole)
        {
            await _roleManager.CreateAsync(appRole);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            AppRole? role = await _roleManager.FindByIdAsync(id.ToString());

            ViewBag.Users = await _userManager.GetUsersInRoleAsync(role?.Name ?? "");

            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AppRole model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id.ToString());

            role.Name = model.Name;

            await _roleManager.UpdateAsync(role);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());

            await _roleManager.DeleteAsync(role);

            return RedirectToAction("Index");
        }

    }
}
