using System.ComponentModel.DataAnnotations;

namespace H2H.Models
{
    public class Book
    {
        [Key] public int Id { get; set; }

        [Required(ErrorMessage = "All books must have a title.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Maximum title length is 150 characters.")]
        public string Title { get; set; }

        [StringLength(255, ErrorMessage = "Maximum description length is 255 characters.")]
        public string Description { get; set; }
    }
}