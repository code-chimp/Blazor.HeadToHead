using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using H2H.Blazor.UI.Models;
using H2H.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace H2H.Blazor.UI.Pages
{
    public partial class Books
    {
        private List<Book> books;
        private List<SelectListItem> publisherOptions;
        private bool showEditDialog;
        private bool showBookDetailsDialog;
        private BookEditViewModel editViewModel;
        private BookDetailsViewModel detailsViewModel;

        private async Task RefreshBooksList()
        {
            books = (List<Book>) await @Service.Books
                .GetAllAsync(includeProperties: "BookDetail,Publisher,BookAuthors.Author");
        }

        protected override async Task OnInitializedAsync()
        {
            await RefreshBooksList();

            var publishers = await @Service.Publishers.GetAllAsync();
            publisherOptions = publishers
                .Select(_ => new SelectListItem
                {
                    Value = _.Id.ToString(),
                    Text = _.Name,
                }).ToList();
        }

        private async Task Upsert(int? id)
        {
            var book = new BookViewModel();

            if (id != null)
            {
                var entity = await @Service.Books.GetAsync(id.Value);

                if (entity != null)
                {
                    book.Id = entity.Id;
                    book.Title = entity.Title;
                    book.ISBN = entity.ISBN;
                    book.Price = entity.Price;
                    book.PublisherId = entity.PublisherId.ToString();
                }
            }

            editViewModel = new BookEditViewModel
            {
                Book = book,
                Publishers = publisherOptions
            };

            showEditDialog = true;
        }

        private async Task SaveBook()
        {
            showEditDialog = false;

            // TODO: Wire validation to the sub-model so this correctly throws up
            var publisherId = string.IsNullOrEmpty(editViewModel.Book.PublisherId)
                ? 0
                : int.Parse(editViewModel.Book.PublisherId);

            if (editViewModel.Book.Id == 0)
            {
                var book = new Book
                {
                    Title = editViewModel.Book.Title,
                    ISBN = editViewModel.Book.ISBN,
                    Price = editViewModel.Book.Price,
                    PublisherId = publisherId
                };

                await @Service.Books.AddAsync(book);
            }
            else
            {
                var book = await @Service.Books.GetAsync(editViewModel.Book.Id);

                book.Title = editViewModel.Book.Title;
                book.ISBN = editViewModel.Book.ISBN;
                book.Price = editViewModel.Book.Price;
                book.PublisherId = publisherId;

                await @Service.Books.UpdateAsync(book);
            }

            await @Service.SaveAsync();

            await RefreshBooksList();
        }

        private async Task Delete(int id)
        {
            await @Service.Books.RemoveAsync(id);
            @Service.Save();

            await RefreshBooksList();
        }

        private async Task Details(int id)
        {
            var book = await @Service.Books.GetAsync(id);

            if (book != null)
            {
                detailsViewModel = new BookDetailsViewModel
                {
                    Book = book,
                    BookDetail = book.BookDetailId == null
                        ? new BookDetail()
                        : @Service.BookDetails.Get(book.BookDetailId.Value)
                };

                showBookDetailsDialog = true;
            }
        }

        private async Task SaveDetails()
        {
            showBookDetailsDialog = false;

            if (detailsViewModel.BookDetail.Id == 0)
            {
                await @Service.BookDetails.AddAsync(detailsViewModel.BookDetail);
                @Service.Save();

                detailsViewModel.Book.BookDetailId = detailsViewModel.BookDetail.Id;
                await @Service.Books.UpdateAsync(detailsViewModel.Book);
            }
            else
            {
                await @Service.BookDetails.UpdateAsync(detailsViewModel.BookDetail);
            }

            @Service.Save();

            await RefreshBooksList();
        }

        private void CloseModals()
        {
            showEditDialog = false;
            showBookDetailsDialog = false;
            editViewModel = null;
            detailsViewModel = null;
        }
    }
}
