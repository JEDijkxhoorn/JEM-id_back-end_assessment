using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace JEM_id_assessment.Models
{
    public class Article
    {
        [Key]
        [Required]
        [MaxLength(13)]
        public string CodeId { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [Required]
        public int PotSize { get; set; }

        [Required]
        public int PlantHeight { get; set; }

        public string? Color { get; set; }

        [Required]
        public ProductGroup ProductGroup { get; set; }
    }
}
