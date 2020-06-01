using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace H2H.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "All books must have a title.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Maximum title length is 150 characters.")]
        public string Title { get; set; }

        public string ISBN { get; set; }

        public double Price { get; set; }

        [DisplayName("Details")]
        [ForeignKey("BookDetail")]
        public int? BookDetailId { get; set; }
        public virtual BookDetail BookDetail { get; set; }
        
        [DisplayName("Publisher")]
        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}