using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H2H.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        
        [DisplayName("Category")]
        public string Name { get; set; }
    }
}