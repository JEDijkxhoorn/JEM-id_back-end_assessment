using System.ComponentModel.DataAnnotations;

namespace JEM_id_assessment.Models
{
    public class ProductGroup
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
