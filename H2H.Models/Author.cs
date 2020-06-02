using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace H2H.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please give us at least a first initial.")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "We require a last name for all authors.")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        public string Location { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
