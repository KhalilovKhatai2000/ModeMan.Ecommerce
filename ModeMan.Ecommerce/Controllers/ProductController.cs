using Microsoft.AspNetCore.Mvc;
using ModeMan.Ecommerce.Services.Abstract;

namespace ModeMan.Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View(_service.GetAll().ToList());
        }
    }
}
