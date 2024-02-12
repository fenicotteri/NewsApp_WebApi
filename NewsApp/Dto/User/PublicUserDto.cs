using System.ComponentModel.DataAnnotations;

namespace NewsApp.Dto
{
    public class PublicUserDto
    {
        /// <example> 1 </example>
        [Required]
        public string Id { get; set; } = string.Empty;

        /// <example>Мария</example>
        [Required]
        public string? FirstName { get; set; }

        /// <example>Гаврилова</example>
        [Required]
        public string? LastName { get; set; }

        /// <example>mag2003tag@test.ru</example>
        [Required]
        public string? Email { get; set; }

        /// <example>/images/1685004747837-194473147.png</example>
        [Required]
        public string? FilePath { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

    }
}
