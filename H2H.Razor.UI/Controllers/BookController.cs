using System.Linq;
using System.Threading.Tasks;
using H2H.DataAccess.Repository.Contracts;
using H2H.Models;
using H2H.Razor.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var books = await _service.Books
                .GetAllAsync(includeProperties: "Publisher,BookAuthors.Author");

            return View(books);
        }

        public IActionResult Upsert(int? id)
        {
            var book = (id == null ?
                new Book() :
                _service.Books.GetFirstOrDefault(_ => _.Id == id)) ?? new Book();

            var publishers = _service.Publishers
                .GetAll()
                .Select(_ => new SelectListItem
                {
                    Value = _.Id.ToString(),
                    Text = _.Name,
                }).ToList();

            var viewModel = new BookViewModel
            {
                Book = book,
                Publishers = publishers
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(BookViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (vm.Book.Id == 0)
            {
                await _service.Books.AddAsync(vm.Book);
            }
            else
            {
                await _service.Books.UpdateAsync(vm.Book);
            }

            await _service.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var book = await _service.Books.GetFirstOrDefaultAsync(_ => _.Id == id);

            if (book != null)
            {
                _service.Books.Remove(book);
                await _service.SaveAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _service
                .Books
                .GetAsync(id);

            if (book != null)
            {
                book.BookDetail = book.BookDetailId == null ?
                    new BookDetail() :
                    _service.BookDetails.Get(book.BookDetailId.Value);

                var viewModel = new BookViewModel
                {
                    Book = book
                };

                return View(viewModel);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(BookViewModel viewModel)
        {
            if (viewModel.Book.BookDetail.Id == 0)
            {
                await _service.BookDetails.AddAsync(viewModel.Book.BookDetail);
            }
            else
            {
                await _service.BookDetails.UpdateAsync(viewModel.Book.BookDetail);
            }

            await _service.SaveAsync();

            var book = await _service.Books.GetAsync(viewModel.Book.Id);
            book.BookDetailId = viewModel.Book.BookDetail.Id;

            await _service.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ManageAuthors(int id)
        {
            var vm = new BookAuthorViewModel
            {
                BookAuthors = _service
                    .BookAuthors
                    .GetAll(filter: _ => _.BookId == id, includeProperties:"Book,Author")
                    .ToList(),
                BookAuthor = new BookAuthor {BookId = id},
                Book = _service.Books.Get(id)
            };

            var assignedAuthors = vm.BookAuthors.Select(_ => _.Author.Id).ToList();
            var authors = _service.Authors.GetAll(_ => !assignedAuthors.Contains(_.Id)).ToList();

            vm.Authors = authors.Select(_ => new SelectListItem {Value = _.Id.ToString(), Text = _.FullName});

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageAuthors(BookAuthorViewModel viewModel)
        {
            if (viewModel.BookAuthor.BookId != 0 && viewModel.BookAuthor.AuthorId != 0)
            {
                await _service.BookAuthors.AddAsync(viewModel.BookAuthor);
                await _service.SaveAsync();
            }

            return RedirectToAction(
                nameof(ManageAuthors),
                new {@id = viewModel.BookAuthor.BookId}
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveAuthors(int authorId, BookAuthorViewModel viewModel)
        {
            var bookAuthorQuery = await _service
                .BookAuthors
                .GetAllAsync(_ => _.AuthorId == authorId && _.BookId == viewModel.Book.Id);

            if (bookAuthorQuery.Any())
            {
                var bookAuthor = bookAuthorQuery.First();
                _service.BookAuthors.Remove(bookAuthor);

                await _service.SaveAsync();
            }

            return RedirectToAction(
                nameof(ManageAuthors),
                new {@id = viewModel.Book.Id}
            );
        }
    }
}
