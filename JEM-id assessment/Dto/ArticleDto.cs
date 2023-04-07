using JEM_id_assessment.Models;
using System.ComponentModel.DataAnnotations;

namespace JEM_id_assessment.Dto
{
    public class ArticleDto
    {
        public string CodeId { get; set; }

        public string Name { get; set; }

        public int PotSize { get; set; }

        public int PlantHeight { get; set; }

        public string? Color { get; set; }
    }
}
