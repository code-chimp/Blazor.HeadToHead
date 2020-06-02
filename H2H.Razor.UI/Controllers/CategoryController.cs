using System.Threading.Tasks;
using H2H.DataAccess.Repository.Contracts;
using H2H.Models;
using Microsoft.AspNetCore.Mvc;

namespace H2H.Razor.UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _service;

        public CategoryController(IUnitOfWork service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _service.Categories.GetAllAsync();

            return View(categories);
        }

        public IActionResult Upsert(int? id)
        {
            var category = id == null ?
                new Category() :
                _service.Categories.GetFirstOrDefault(_ => _.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            if (category.Id == 0)
            {
                await _service.Categories.AddAsync(category);
            }
            else
            {
                await _service.Categories.UpdateAsync(category);
            }

            await _service.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _service.Categories.GetAsync(id);

            if (category != null)
            {
                _service.Categories.Remove(category);
                await _service.SaveAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
