using System.Collections.Generic;
using H2H.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace H2H.Razor.UI.Models
{
    public class BookAuthorViewModel
    {
        public Book Book { get; set; }
        public BookAuthor BookAuthor { get; set; }

        public IEnumerable<BookAuthor> BookAuthors { get; set; }
        public IEnumerable<SelectListItem> Authors { get; set; }
    }
}
