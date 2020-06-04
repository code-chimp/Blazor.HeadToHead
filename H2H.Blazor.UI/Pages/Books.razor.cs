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
        private bool showManageAuthorsDialog;
        private BookEditViewModel editVM;
        private BookDetailsViewModel detailsVM;
        private BookAuthorsViewModel bookAuthorsVM;

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

            editVM = new BookEditViewModel
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
            var publisherId = string.IsNullOrEmpty(editVM.Book.PublisherId)
                ? 0
                : int.Parse(editVM.Book.PublisherId);

            if (editVM.Book.Id == 0)
            {
                var book = new Book
                {
                    Title = editVM.Book.Title,
                    ISBN = editVM.Book.ISBN,
                    Price = editVM.Book.Price,
                    PublisherId = publisherId
                };

                await @Service.Books.AddAsync(book);
            }
            else
            {
                var book = await @Service.Books.GetAsync(editVM.Book.Id);

                book.Title = editVM.Book.Title;
                book.ISBN = editVM.Book.ISBN;
                book.Price = editVM.Book.Price;
                book.PublisherId = publisherId;

                await @Service.Books.UpdateAsync(book);
            }

            await @Service.SaveAsync();

            await RefreshBooksList();
        }

        private async Task Delete(int bookId)
        {
            var book = await @Service.Books.GetAsync(bookId);

            if (book.BookDetailId != null)
            {
                await @Service.BookDetails.RemoveAsync(book.BookDetailId.Value);
            }

            await @Service.Books.RemoveAsync(bookId);
            @Service.Save();

            await RefreshBooksList();
        }

        private async Task Details(int id)
        {
            var book = await @Service.Books.GetAsync(id);

            if (book != null)
            {
                detailsVM = new BookDetailsViewModel
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

            if (detailsVM.BookDetail.Id == 0)
            {
                await @Service.BookDetails.AddAsync(detailsVM.BookDetail);
                @Service.Save();

                detailsVM.Book.BookDetailId = detailsVM.BookDetail.Id;
                await @Service.Books.UpdateAsync(detailsVM.Book);
            }
            else
            {
                await @Service.BookDetails.UpdateAsync(detailsVM.BookDetail);
            }

            @Service.Save();

            await RefreshBooksList();
        }

        private async Task ManageAuthors(int id)
        {
            var book = await @Service.Books.GetAsync(id);

            if (book != null)
            {
                bookAuthorsVM = new BookAuthorsViewModel
                {
                    Book = book,
                    BookAuthors = @Service
                        .BookAuthors
                        .GetAll(_ => _.BookId == id, includeProperties: "Book,Author")
                        .ToList(),
                    BookAuthorVM = new BookAuthorViewModel {BookId = id, AuthorId = ""}
                };

                var assignedAuthors = bookAuthorsVM.BookAuthors
                    .Select(_ => _.Author.Id)
                    .ToList();
                var authors = @Service.Authors
                    .GetAll(_ => !assignedAuthors.Contains(_.Id))
                    .ToList();

                bookAuthorsVM.Authors = authors
                    .Select(_ => new SelectListItem
                    {
                        Value = _.Id.ToString(),
                        Text = _.FullName
                    });

                showManageAuthorsDialog = true;
            }
        }

        private async Task RemoveAuthor(int authorId)
        {
            var (bookId, _) = bookAuthorsVM;

            var bookAuthorQuery = await @Service
                .BookAuthors
                .GetAllAsync(
                    _ => _.AuthorId == authorId && _.BookId == bookId,
                    includeProperties: "Author");

            if (bookAuthorQuery.Any())
            {
                var bookAuthor = bookAuthorQuery.First();
                @Service.BookAuthors.Remove(bookAuthor);
                @Service.Save();

                bookAuthorsVM.BookAuthors = bookAuthorsVM
                    .BookAuthors
                    .Where(_ => _.AuthorId != authorId);

                bookAuthorsVM.Authors = bookAuthorsVM
                    .Authors
                    .Append(new SelectListItem
                    {
                        Value = bookAuthor.AuthorId.ToString(),
                        Text = bookAuthor.Author.FullName
                    });

                await RefreshBooksList();
            }
        }

        private async Task SaveAuthor()
        {
            var (bookId, authorId) = bookAuthorsVM;

            if (!string.IsNullOrEmpty(authorId) && bookId != 0)
            {
                var bookAuthor = new BookAuthor
                {
                    BookId = bookId,
                    AuthorId = int.Parse(authorId)
                };
                await @Service.BookAuthors.AddAsync(bookAuthor);
                @Service.Save();

                bookAuthorsVM.Authors = bookAuthorsVM.Authors
                    .Where(_ => _.Value != authorId);

                bookAuthorsVM.BookAuthors = (await @Service
                        .BookAuthors
                        .GetAllAsync(_ => _.BookId == bookId,
                            includeProperties: "Book,Author")
                    ).ToList();

                bookAuthorsVM.BookAuthorVM = new BookAuthorViewModel
                {
                    BookId = bookId,
                    AuthorId = ""
                };

                await RefreshBooksList();
            }
        }

        private void CloseModals()
        {
            showEditDialog = false;
            showBookDetailsDialog = false;
            showManageAuthorsDialog = false;
        }
    }
}
