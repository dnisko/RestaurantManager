using System.ComponentModel.DataAnnotations;

namespace DTOs.Category
{
    public class UpdateCategoryDto
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
