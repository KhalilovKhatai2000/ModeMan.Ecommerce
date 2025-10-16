using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using ModeMan.Ecommerce.Entities;
using ModeMan.Ecommerce.Enums;
using ModeMan.Ecommerce.Services.Abstract;

namespace ModeMan.Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IImageService<Product> _imageService;
        private readonly ICategoryService _categoryService;

        public ProductController(
            IProductService productService, 
            IImageService<Product> imageService,
            ICategoryService categoryService)
        {
            _productService = productService;
            _imageService = imageService;
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
           List<Product>? products = _productService.GetAll()
                .Include(p => p.Category)
                .Include(p => p.Images)
                .Include(p => p.OrderItems)
                .ToList();

            return View(products);
        }

        public IActionResult Create()
        { 
            ViewBag.Categories = new SelectList(_categoryService.GetAll().ToList(), 
                                                                        "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {

                var newProduct = await _productService.CreateAsync(product);

                newProduct.Images = await _imageService.CreateBulkImageAsync(files, ImageType.Product, newProduct);

                return RedirectToAction("Index");
            }

            return View(product);
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var product = await _productService.GetByExpressionAsync(
                x => x.Id == id, 
                x => x.Category, 
                x => x.Images, 
                x => x.OrderItems
                );

            ViewBag.Categories = new SelectList(
                _categoryService.GetAll().ToList(),
                "Id", "Name", product.CategoryId);

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Product model, List<IFormFile>? files)
        {
            if (ModelState.IsValid)
            {
                Product entity = await _productService.GetByExpressionAsync(
                    x => x.Id == model.Id,
                    x => x.Category,
                    x => x.Images,
                    x => x.OrderItems
                    );

                entity.Description = model.Description;

                if (model.CategoryId != null)
                {
                    entity.CategoryId = model.CategoryId;
                }

                if (files != null && files.Count > 0)
                {
                    var imageList = await _imageService.GetAllAsync();
                    
                    var fileNameList = imageList.Select(i => i.Url).ToList();

                    _imageService.DeleteBulkImage(fileNameList);

                    var images = await _imageService
                        .CreateBulkImageAsync(files, ImageType.Product, entity);

                    foreach (var image in images)
                    {
                        image.ProductId = entity.Id;
                    }
                }

                _productService.Update(entity);

                return RedirectToAction("Index");
            }

            return View(model);
        }

    }
}
