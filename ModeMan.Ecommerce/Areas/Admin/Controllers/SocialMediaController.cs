using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModeMan.Ecommerce.Entities;
using ModeMan.Ecommerce.Enums;
using ModeMan.Ecommerce.Services.Abstract;

namespace ModeMan.Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SocialMediaController : Controller
    {
        private readonly ISocialMediaService _socialMediaService;

        public SocialMediaController(ISocialMediaService socialMediaService)
        {
            _socialMediaService = socialMediaService;
        }

        public IActionResult Index()
        {
            List<SocialMedia> SocialMedias = _socialMediaService.GetAll().ToList();

            return View(SocialMedias);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(SocialMedia socialMedia)
        {
            if (ModelState.IsValid)
            {
                SocialMedia created = await _socialMediaService.CreateAsync(socialMedia);

                return RedirectToAction("Index");
            }

            return View(socialMedia);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id) => 
            View(await _socialMediaService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Update(SocialMedia socialMedia)
        {
            if (ModelState.IsValid)
            {
                _socialMediaService.Update(socialMedia);

                return RedirectToAction("Index");
            }

            return View(socialMedia);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            _socialMediaService.Delete(_socialMediaService.GetById(id));

            return RedirectToAction("Index");
        }
    }
}
