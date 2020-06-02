using System.Threading.Tasks;
using H2H.DataAccess.Repository.Contracts;
using H2H.Models;
using Microsoft.AspNetCore.Mvc;

namespace H2H.Razor.UI.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IUnitOfWork _service;

        public PublisherController(IUnitOfWork service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var publishers = await _service.Publishers.GetAllAsync();

            return View(publishers);
        }

        public IActionResult Upsert(int? id)
        {
            var publisher = id == null ?
                new Publisher() :
                _service.Publishers.GetFirstOrDefault(_ => _.Id == id);

            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return View(publisher);
            }

            if (publisher.Id == 0)
            {
                await _service.Publishers.AddAsync(publisher);
            }
            else
            {
                await _service.Publishers.UpdateAsync(publisher);
            }

            await _service.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var publisher = await _service.Publishers.GetAsync(id);

            if (publisher != null)
            {
                _service.Publishers.Remove(publisher);
                await _service.SaveAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
