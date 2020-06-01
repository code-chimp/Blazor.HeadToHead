using System.Collections.Generic;
using System.Threading.Tasks;
using H2H.Models;

namespace H2H.Blazor.UI.Pages
{
    public partial class Books
    {
        private List<Book> books;
        private bool ShowDialog;
        private Book viewModel;

        protected override async Task OnInitializedAsync()
        {
            books = (List<Book>) await @Service.Books.GetAllAsync();
        }

        private void AddBook()
        {
            viewModel = new Book
            {
                Id = 0
            };

            ShowDialog = true;
        }

        private void EditBook(Book book)
        {
            viewModel = new Book
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description
            };

            ShowDialog = true;
        }

        private async Task SaveBook()
        {
            ShowDialog = false;

            if (viewModel.Id == 0)
            {
                await @Service.Books.AddAsync(viewModel);
            }
            else
            {
                await @Service.Books.UpdateAsync(viewModel);
            }

            await @Service.SaveAsync();

            books = (List<Book>) await @Service.Books.GetAllAsync();
        }

        private async Task DeleteBook()
        {
            ShowDialog = false;
            
            await @Service.Books.RemoveAsync(viewModel.Id);
            await @Service.SaveAsync();

            books = (List<Book>) await @Service.Books.GetAllAsync();
        }

        private void CloseModals()
        {
            ShowDialog = false;
            viewModel = null;
        }
    }
}