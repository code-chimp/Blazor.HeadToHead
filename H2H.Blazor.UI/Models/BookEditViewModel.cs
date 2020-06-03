using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace H2H.Blazor.UI.Models
{
    public class BookEditViewModel
    {
        public BookViewModel Book { get; set; }
        public IEnumerable<SelectListItem> Publishers { get; set; }
    }
}
