using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace H2H.Models
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}