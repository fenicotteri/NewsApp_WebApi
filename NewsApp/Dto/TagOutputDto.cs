using System.ComponentModel.DataAnnotations;

namespace NewsApp.Dto
{
    public class TagOutputDto
    {
        /// <example> 1 </example>
        [Required]
        public int Id { get; set; }

        /// <example>#tag</example>
        [Required]
        public string Value { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}
