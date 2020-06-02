using System.Threading.Tasks;
using H2H.DataAccess.Repository.Contracts;
using H2H.Models;
using Microsoft.AspNetCore.Mvc;

namespace H2H.Razor.UI.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IUnitOfWork _service;

        public AuthorController(IUnitOfWork service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var authors = await _service.Authors.GetAllAsync();

            return View(authors);
        }

        public IActionResult Upsert(int? id)
        {
            var author = id == null ?
                new Author() :
                _service.Authors.GetFirstOrDefault(_ => _.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            if (author.Id == 0)
            {
                await _service.Authors.AddAsync(author);
            }
            else
            {
                await _service.Authors.UpdateAsync(author);
            }

            await _service.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var author = await _service.Authors.GetAsync(id);

            if (author != null)
            {
                _service.Authors.Remove(author);
                await _service.SaveAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
