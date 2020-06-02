using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H2H.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "We are going to need a name for the category")]
        [DisplayName("Category")]
        public string Name { get; set; }
    }
}
