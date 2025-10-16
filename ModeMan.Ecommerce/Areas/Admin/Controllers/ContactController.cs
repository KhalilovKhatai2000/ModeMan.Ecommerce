using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModeMan.Ecommerce.Entities;
using ModeMan.Ecommerce.Enums;
using ModeMan.Ecommerce.Services.Abstract;

namespace ModeMan.Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public IActionResult Index()
        {
            List<Contact> Contacts = _contactService.GetAll().ToList();

            return View(Contacts);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Contact Contact)
        {
            if (ModelState.IsValid)
            {
                Contact created = await _contactService.CreateAsync(Contact);

                return RedirectToAction("Index");
            }

            return View(Contact);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            Contact entity = await _contactService.GetByIdAsync(id);

            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Contact Contact)
        {
            if (ModelState.IsValid)
            {
                _contactService.Update(Contact);

                return RedirectToAction("Index");
            }

            return View(Contact);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            _contactService.Delete(_contactService.GetById(id));

            return RedirectToAction("Index");
        }
    }
}
