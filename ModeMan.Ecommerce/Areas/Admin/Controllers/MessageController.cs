using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModeMan.Ecommerce.Entities;
using ModeMan.Ecommerce.Services.Abstract;

namespace ModeMan.Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public IActionResult Index()
        {
            List<Message> messages = _messageService.GetAll().ToList();

            return View(messages);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Message message)
        {
            if (ModelState.IsValid)
            {
                await _messageService.CreateAsync(message);

                return RedirectToAction("Index");
            }

            return View(message);
        }

        [HttpGet]
        public IActionResult Update(Guid id) => View(_messageService.GetById(id));

        [HttpPost]
        public IActionResult Update(Message message)
        {
            if (ModelState.IsValid)
            {
                _messageService.Update(message);

                return RedirectToAction("Index");
            }

            return View(message);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            _messageService.Delete(_messageService.GetById(id));

            return RedirectToAction("Index");
        }
    }
}
