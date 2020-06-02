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
        private BookEditViewModel editViewModel;
        private BookDetail detailsViewModel;

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

        // private void Details(BookDetail bookDetail)
        // {
        //     detailsViewModel = new BookDetail
        //     {
        //         Id = bookDetail.Id,
        //         Description = bookDetail.Description,
        //         NumberOfChapters = bookDetail.NumberOfChapters,
        //         NumberOfPages = bookDetail.NumberOfPages,
        //         Weight = bookDetail.Weight
        //     };
        //
        //     showDetailsDialog = true;
        // }

        // private async Task SaveDetails()
        // {
        //     showDetailsDialog = false;
        //
        //     if (detailsViewModel.Id == 0)
        //     {
        //         await @Service.BookDetails.AddAsync(detailsViewModel);
        //         @Service.Save();
        //         viewModel.BookDetailId = detailsViewModel.Id;
        //     }
        //     else
        //     {
        //         await @Service.BookDetails.UpdateAsync(detailsViewModel);
        //     }
        //
        //     @Service.Save();
        //
        //     books = (List<Book>) await @Service.Books.GetAllAsync();
        // }

        // private async Task DeleteDetails()
        // {
        //     showDetailsDialog = false;
        //
        //     await @Service.BookDetails.RemoveAsync(detailsViewModel.Id);
        //     await @Service.SaveAsync();
        //
        //     books = (List<Book>) await @Service.Books.GetAllAsync();
        // }

        private void CloseModals()
        {
            showEditDialog = false;
            editViewModel = null;
        }
    }
}
