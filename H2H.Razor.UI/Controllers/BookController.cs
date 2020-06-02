using System.Threading.Tasks;
using H2H.DataAccess.Repository.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace H2H.Razor.UI.Controllers
{
    public class BookController : Controller
    {
        private readonly IUnitOfWork _service;

        public BookController(IUnitOfWork service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _service.Books.GetAllAsync(includeProperties: "Publisher,BookAuthors.Author");

            return View(books);
        }
    }
}
