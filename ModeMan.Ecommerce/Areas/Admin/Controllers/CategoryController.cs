using Microsoft.AspNetCore.Mvc;
using ModeMan.Ecommerce.Entities;
using ModeMan.Ecommerce.Services.Abstract;

namespace ModeMan.Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            List<Category> categories = _categoryService.GetAll().ToList();

            return View(categories);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if(ModelState.IsValid)
            {
                await _categoryService.CreateAsync(category);

                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult Update(Guid id) => View(_categoryService.GetById(id));

        [HttpPost]
        public IActionResult Update(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.Update(category);

                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            _categoryService.Delete(_categoryService.GetById(id));

            return RedirectToAction("Index");
        }
    }
}
