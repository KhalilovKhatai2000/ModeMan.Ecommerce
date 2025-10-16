using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModeMan.Ecommerce.Entities;
using ModeMan.Ecommerce.Enums;
using ModeMan.Ecommerce.Services.Abstract;

namespace ModeMan.Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IImageService<Slider> _imageService;

        public SliderController(ISliderService sliderService, IImageService<Slider> imageService)
        {
            _sliderService = sliderService;
            _imageService = imageService;
        }

        public IActionResult Index()
        {
            List<Slider> sliders = _sliderService.GetAll()
                                                 .Include(s => s.Image)
                                                 .ToList();

            return View(sliders);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Slider slider, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                Slider created = await _sliderService.CreateAsync(slider);


                if (file.Length > 0)
                    await _imageService.CreateImageAsync(file, ImageType.Slider, slider);

                return RedirectToAction("Index");
            }

            return View(slider);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid? id)
        {
            Slider entity = await _sliderService
                .GetByExpressionAsync(x => x.Id == id, x => x.Image);

            if(id is null)
            {
                return RedirectToAction("Index");
            }

            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Slider slider, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                _sliderService.Update(slider);

                if(file.Length > 0)
                    await _imageService.CreateImageAsync(file, ImageType.Slider, slider);

                return RedirectToAction("Index");
            }

            return View(slider);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            _sliderService.Delete(_sliderService.GetById(id));

            return RedirectToAction("Index");
        }
    }
}
