using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H2H.Models
{
    public class BookDetail
    {
        [Key]
        public int Id { get; set; }
        
        [DisplayName("# of Chapters")]
        public int NumberOfChapters { get; set; }
        
        [DisplayName("# of Pages")]
        public int NumberOfPages { get; set; }
        
        [DisplayName("Weight (kg)")]
        public double Weight { get; set; }
        
        [StringLength(255, ErrorMessage = "Maximum description length is 255 characters.")]
        public string Description { get; set; }

        public virtual Book Book { get; set; }
    }
}