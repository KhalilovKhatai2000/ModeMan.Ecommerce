using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModeMan.Ecommerce.Entities;
using ModeMan.Ecommerce.Enums;
using ModeMan.Ecommerce.Services.Abstract;

namespace ModeMan.Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerController : Controller
    {
        private readonly IBannerService _bannerService;
        private readonly IImageService<Banner> _imageService;

        public BannerController(IBannerService BannerService, IImageService<Banner> imageService)
        {
            _bannerService = BannerService;
            _imageService = imageService;
        }

        public IActionResult Index()
        {
            List<Banner> Banners = _bannerService.GetAll()
                                                 .Include(s => s.Image)
                                                 .ToList();

            return View(Banners);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Banner banner, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                Banner created = await _bannerService.CreateAsync(banner);


                if (file.Length > 0)
                    await _imageService.CreateImageAsync(file, ImageType.Banner, banner);

                return RedirectToAction("Index");
            }

            return View(banner);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid? id)
        {
            Banner entity = await _bannerService
                .GetByExpressionAsync(x => x.Id == id, x => x.Image);

            if (id is null)
            {
                return RedirectToAction("Index");
            }

            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Banner banner, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                _bannerService.Update(banner);

                if (file.Length > 0)
                    await _imageService.CreateImageAsync(file, ImageType.Banner, banner);

                return RedirectToAction("Index");
            }

            return View(banner);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            _bannerService.Delete(_bannerService.GetById(id));

            return RedirectToAction("Index");
        }
    }
}
