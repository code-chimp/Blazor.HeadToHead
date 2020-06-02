using System.ComponentModel.DataAnnotations;

namespace H2H.Blazor.UI.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "All books must have a title.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Maximum title length is 150 characters.")]
        public string Title { get; set; }

        public string ISBN { get; set; }

        public double Price { get; set; }

        public string PublisherId { get; set; }
    }
}
