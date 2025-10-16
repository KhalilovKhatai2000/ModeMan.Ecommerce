using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModeMan.Ecommerce.Data;
using ModeMan.Ecommerce.Entities;
using ModeMan.Ecommerce.Models;
using ModeMan.Ecommerce.Services.Abstract;

namespace ModeMan.Ecommerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly ModeManDbContext _context;
        private readonly IEmailService _emailService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(
            ModeManDbContext context, 
            IEmailService emailService, 
            UserManager<AppUser> userManager, 
            RoleManager<AppRole> roleManager, 
            SignInManager<AppUser> signInManager)
        {
            _context = context;
            _emailService = emailService;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Login() => View();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                AppUser? entity = await _userManager.FindByEmailAsync(model.Email);
                var result = await _signInManager.PasswordSignInAsync(
                    entity, 
                    model.Password, 
                    model.RememberMe, 
                    true);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", $"Zehmet olmasa {entity.LockoutEnd} gozleyin...");
                }

                ModelState.AddModelError("","Email ve ya sifre yalnisdir.");
                return View(model);
            }

            ModelState.AddModelError("", "Email ve ya sifre yalnisdir.");
            return View(model);
        }
        

        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser newUser = new AppUser
                {
                    UserName = model.UserName,
                    FullName = string.Concat(model.UserName, " ", model.Surname),
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(newUser, model.Password);

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

                var url = Url.Action("ConfirmationEmailSend", "Account", new
                {
                    userId = newUser.Id,
                    token = token
                }, Request.Scheme);

                await _emailService.SendEmailAsync(
                    model.Email, 
                    "Sifre tesdiq emaili.",
                    $"Zehmet olmasa <a href='{url}'>Linki</a> tesdiqleyin");

                if (result.Succeeded)
                {
                    return RedirectToAction("EmailSend", "Account");
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

            }
            return View(model);
        }

        public IActionResult EmailSend()
        {
            return View();
        }

        public async Task<IActionResult> ConfirmationEmailSend(Guid userId, string token)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId.ToString());
            var result = _userManager.ConfirmEmailAsync(user, token);

            if(result.IsCompleted)
            {
                return RedirectToAction("ConfirmEmailSuccessfully", "Account");
            }
            return View("EmailSend");
        }

        public IActionResult ConfirmEmailSuccessfully()
        {
            return View();
        }

    }
}
