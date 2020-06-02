using System.Collections.Generic;
using H2H.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace H2H.Razor.UI.Models
{
    public class BookViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<SelectListItem> Publishers { get; set; }
    }
}
