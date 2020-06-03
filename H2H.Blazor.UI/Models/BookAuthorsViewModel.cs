using System.Collections.Generic;
using H2H.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace H2H.Blazor.UI.Models
{
    public class BookAuthorsViewModel
    {
        public Book Book { get; set; }
        public BookAuthorViewModel BookAuthorVM { get; set; }

        public IEnumerable<BookAuthor> BookAuthors { get; set; }
        public IEnumerable<SelectListItem> Authors { get; set; }

        public void Deconstruct(out int bookId, out string authorId)
        {
            bookId = Book.Id;
            authorId = BookAuthorVM.AuthorId;
        }
    }
}
